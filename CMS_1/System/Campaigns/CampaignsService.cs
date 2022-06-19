using CMS_1.Models;
using CMS_1.Models.Campaign;
using CMS_1.System.Campaign;
using System.Drawing;
using BarcodeLib;
using System.Drawing.Imaging;
using QRCoder;
using SkiaSharp;
using CMS_1.Models.Campaigns;

namespace CMS_1.System.Campaign
{
    public class CampaignsService : ICampaignsService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;
        

        public CampaignsService(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _appDbContext = appDbContext;
        }

        public async Task<CreateCampaignResponse> CreateCampaign(CreateCampaignRequest model)
        {
            try
            {
                var campaign = new Campaignn
                {
                    Name = model.Name,
                    AutoUpdate = model.AutoUpdate,
                    JoinOnlyOne = model.JoinOnlyOne,
                    Decription = model.Decription,
                    CountCode = model.CountCode,
                    IdProgramSize = model.IdProgramSize,
                    StartDay = model.timeFrame.StartDay.Date,
                    StartTime = model.timeFrame.StartTime.TimeOfDay,
                    EndDay = model.timeFrame.EndDay.Date,
                    EndTime = model.timeFrame.EndTime.TimeOfDay,

                };
                int IdCampaignTemp;
                if (_appDbContext.Campaigns.Count()==0)
                {
                    IdCampaignTemp = 1;
                }
                else
                {
                    IdCampaignTemp = _appDbContext.Campaigns.Max(x => x.Id) + 1;
                }

                _appDbContext.Add(campaign);
                _appDbContext.SaveChanges();
                var barcodes = new GenerateNewBarcodeRequest
                {
                    IdCampaign = _appDbContext.Campaigns.Max(x => x.Id),
                    CodeUsageLimit = model.CreateBarcodeRequest.CodeUsageLimit,
                    Unlimited = model.CreateBarcodeRequest.Unlimited,
                    CountCode = model.CountCode,
                    CodeLength = model.CreateBarcodeRequest.CodeLength,
                    Prefix = model.CreateBarcodeRequest.Prefix,
                    Postfix = model.CreateBarcodeRequest.Postfix,
                    IdCharset = model.CreateBarcodeRequest.IdCharset,
                    
                };

                
                for(int i = 0;i< model.CountCode; i++)
                {
                    AutoCreateBarCode(barcodes );
                }

                return new CreateCampaignResponse { Success = true, Message = "The Campaign (campaign name) is created successfully." };
            }
            catch
            {
                return new CreateCampaignResponse { Success = false, Message = "Error" };
            }
        }

        public  ICollection<Campaignn> GetAllCampaigns()
        {
            var ds =  _appDbContext.Campaigns.ToList();
            return ds;
        }
        
        public void AutoCreateBarCode(GenerateNewBarcodeRequest model)
        {
            int postfix = model.CodeLength - model.Prefix.Length;
            
            var barcodestring = new char[postfix];
            var random = new Random();
            var charset = GetTypeOfCharset(model.IdCharset);
            for(int i =0; i< barcodestring.Length; i++)
            {
                barcodestring[i] = charset[random.Next(charset.Length)];
            }
            string finalpostfix = new string(barcodestring);
            var finalbarcode = new string(model.Prefix + finalpostfix);
            CreateBarcodeAndQRcode(finalbarcode);
            var barcode = new Models.Barcode
            {
                IdCampaign = model.IdCampaign,
                Code = finalbarcode,
                BarCode = new string(@"./Images/Barcode/Barcode_" + finalbarcode + ".png"),
                QRcode = new string(@"./Images/QRcode/QRcode_" + finalbarcode + ".png"),
                CodeUsageLimit = model.CodeUsageLimit,
                Unlimited = model.Unlimited,
                CreateDate = DateTime.Now,
                IsScanned = false,
                Used = 0,
                Owner = "",
                Active = true,
                IdCharset = model.IdCharset
                
            };
            _appDbContext.Add(barcode);
            _appDbContext.SaveChanges();
        }
        
        public void CreateBarcodeAndQRcode(string character)
        {
            BarcodeLib.Barcode bc = new BarcodeLib.Barcode();
            int imageWidth = 250;  // barcode image width
            int imageHeight = 110; //barcode image height
            Color foreColor = Color.Black; // Color to print barcode
            Color backColor = Color.Transparent; //background color

            //numeric string to generate barcode
            string dt = character;
            Image barcodeImage = bc.Encode(TYPE.CODE128, dt, foreColor, backColor, imageWidth, imageHeight);

            // Store image in some path with the desired format
            //note: you must have permission to save file in the specified path
            barcodeImage.Save(@"./Images/Barcode/Barcode_" + character + ".png", ImageFormat.Png);
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData qrdata = qr.CreateQrCode(character, QRCodeGenerator.ECCLevel.L);
            QRCode qrCode = new QRCode(qrdata);
            var qrCodeAsBitmap = qrCode.GetGraphic(20);
            Image QRImage = qrCodeAsBitmap;
            QRImage.Save(@"./Images/QRcode/QRcode_" + character + ".png", ImageFormat.Png);
            
        }
        public string GetTypeOfCharset(int type)
        {
            var characters = "";
            if (type == 1)
            {
                characters = "123456789";
            }
            else if (type ==2 )
            {
                characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            else if (type == 3)
            {
                characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            }
            return characters;
        }

        public async Task<GenerateNewBarcodeReqsponse> CreateNewBarcodes(GenerateNewBarcodeRequest model)
        {
            try
            {
                var campaign = _appDbContext.Campaigns.SingleOrDefault(x => x.Id == model.IdCampaign);
                campaign.CountCode = _appDbContext.Barcodes.Count(x=>x.IdCampaign==campaign.Id)+ model.CountCode;
                _appDbContext.Update(campaign);
                _appDbContext.SaveChanges();
                for (int i = 0; i < model.CountCode; i++)
                {
                    AutoCreateBarCode(model);
                }
                return new GenerateNewBarcodeReqsponse { Success = true, Message = "Generated "+model.CountCode+" Barcodes successfully." };
            }
            catch
            {
                return new GenerateNewBarcodeReqsponse { Success = false, Message = "Error" };

            }
        }

        public async Task<GenerateNewBarcodeReqsponse> ChangeStateOfBarcode(int id, bool status)
        {
            
            try
            {
                var code = _appDbContext.Barcodes.SingleOrDefault(x => x.Id == id);
                code.Active = status;
                _appDbContext.Update(code);
                _appDbContext.SaveChanges();
                if (code.Active == true)
                {
                    return new GenerateNewBarcodeReqsponse { Success = true, Message = "The Barcode " + code.Code + " is Activated." };
                }
                else
                {
                    return new GenerateNewBarcodeReqsponse { Success = true, Message = "The Barcode " + code.Code + " is De-activated." };
                }
            }
            catch
            {
                return new GenerateNewBarcodeReqsponse { Success = false, Message = "Error" };
            }
        }

        public  ICollection<BarcodeVM> GetAllBarcodesOfCampaign(int id)
        {
            var allbarcode = _appDbContext.Barcodes.Where(x=>x.IdCampaign==id).ToList();
            var cp = _appDbContext.Campaigns.SingleOrDefault(x=>x.Id==id);
            var listbarcode = new List<BarcodeVM>();
            foreach (var barcode in allbarcode)
            {
                BarcodeVM barcodeVM = new BarcodeVM
                {
                    Id = barcode.Id,
                    Code = barcode.Code,
                    BarCode = barcode.BarCode,
                    QRcode = barcode.QRcode,
                    CreateDate = barcode.CreateDate,
                    ExpiredDate = cp.EndDay,
                    ScannedDate = barcode.ScannedDate,
                    Owner = barcode.Owner,
                    IsScanned = barcode.IsScanned,
                    Active = barcode.Active,
                    campaign = cp.Name
                };
                listbarcode.Add(barcodeVM);
            }
            return listbarcode;
        }

        public async Task<ScanBarcodeResponse> ScanBarcodeForCustomer(int id, string owner)
        {
            try
            {
                var barc = _appDbContext.Barcodes.SingleOrDefault(x => x.Id == id);
                barc.Owner = owner;
                barc.ScannedDate = DateTime.Now;
                barc.Used++;
                barc.IsScanned = true;
                _appDbContext.Update(barc);
                _appDbContext.SaveChanges();
                return new ScanBarcodeResponse { Success = true, Message = "The Barcode "+barc.Code+" is scanned for customer (customer email) by "+ owner };
            }
            catch
            {
                return new ScanBarcodeResponse { Success = false, Message = "Error" };
            }
            
        }

        public ICollection<GiftCampaignMV> GetAllGiftOfCampaign(int id)
        {
            var allgift = _appDbContext.Gifts.Where(x => x.IdCampaign == id).ToList();
            var listgift = new List<GiftCampaignMV>();
            foreach (var giftt in allgift)
            {
                var gc = _appDbContext.GiftCategories.SingleOrDefault(x => x.Id == giftt.IdGiftCategory);
                GiftCampaignMV barcodeVM = new GiftCampaignMV
                {
                    Id = giftt.Id,
                    GiftCode = giftt.GiftCode,
                    CreateDate = giftt.CreateDate,
                    Usagelimit = giftt.UsageLimit,
                    Active = giftt.Active,
                    Used = giftt.Used,
                    GiftName = gc.Name,
                };
                listgift.Add(barcodeVM);
            }
            return listgift;
        }

        public ICollection<RuleOfGiftVM> GetAllRuleOfGiftInCampaign()
        {
            var allrule = _appDbContext.RuleOfGifts.ToList();
            var listruleVM = new List<RuleOfGiftVM>();
            foreach (var rule in allrule)
            {
                var rs = _appDbContext.RepeatSchedules.SingleOrDefault(x=>x.Id==rule.IdIdRepeatSchedule);
                var gc = _appDbContext.GiftCategories.SingleOrDefault(x => x.Id == rule.IdGiftCategory);
                RuleOfGiftVM rol = new RuleOfGiftVM
                {
                    id = rule.Id,
                    RuleName = rule.Name,
                    GiftName = gc.Name,
                    Schedule = rs.Name +": "+ rule.ScheduleData,
                    StartTime = rule.StartTime,
                    EndTime =rule.EndTime,
                    AllDay = rule.AllDay,
                    TotalGift = rule.GiftCount,
                    Probability = rule.Probability,
                    Active = rule.Active,
                    Priority = rule.Priority,
                };
                listruleVM.Add(rol);
            }
            return listruleVM;
        }

        public async Task<RuleOfGiftResponse> CreateNewRuleOfGift(RuleOfGiftRequest model)
        {
            try
            {
                int priority;
                if (_appDbContext.RuleOfGifts.Count() == 0)
                {
                    priority = 1;
                }
                else
                {
                    priority = _appDbContext.RuleOfGifts.Max(x => x.Priority) + 1;
                }
                var rog = new RuleOfGift
                {
                    Name = model.RuleName,
                    GiftCount = model.GiftCount,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    AllDay = model.AllDay,
                    Probability = model.Probability,
                    ScheduleData = model.ScheduleData,
                    Active = true,
                    Priority = priority,
                    IdGiftCategory = model.IdGiftCategory,
                    IdIdRepeatSchedule = model.IdRepeatSchedule
                };
                _appDbContext.Add(rog);
                _appDbContext.SaveChanges();
                return new RuleOfGiftResponse { Success = true, Message = "Create Rule for Gifts successful." };
            }
            catch
            {
                return new RuleOfGiftResponse { Success = false, Message = "Error." };
            }
        }
        public async Task<RuleOfGiftResponse> EditRuleOfGift(RuleOfGiftRequest model, int id)
        {
            try
            {
                var rog = _appDbContext.RuleOfGifts.SingleOrDefault(x => x.Id == id);
                rog.Name = model.RuleName;
                rog.GiftCount = model.GiftCount;
                rog.StartTime = model.StartTime;
                rog.EndTime = model.EndTime;
                rog.AllDay = model.AllDay;
                rog.Probability = model.Probability;
                rog.ScheduleData = model.ScheduleData;
                rog.IdGiftCategory = model.IdGiftCategory;
                rog.IdIdRepeatSchedule = model.IdRepeatSchedule;
                
                _appDbContext.Update(rog);
                _appDbContext.SaveChanges();
                return new RuleOfGiftResponse { Success = true, Message = "Rule for Gifts has been updated." };
            }
            catch
            {
                return new RuleOfGiftResponse { Success = false, Message = "Error." };
            }
        }

        public async Task<RuleOfGiftResponse> ActiveRuleOfGift(bool status, int id)
        {
            try
            {
                var rog = _appDbContext.RuleOfGifts.SingleOrDefault(x=>x.Id== id);
                rog.Active = status;
                _appDbContext.Update(rog);
                _appDbContext.SaveChanges();
                if (rog.Active == true)
                {
                    return new RuleOfGiftResponse { Success = true, Message = "The rule "+rog.Name+" is Activated" };
                }
                else
                {
                    return new RuleOfGiftResponse { Success = true, Message = "The rule "+rog.Name+" is De-activated" };
                }
            }
                
            catch
            {
                return new RuleOfGiftResponse { Success = false, Message = "Error." };
            }
        }

        public async Task<RuleOfGiftResponse> RaiseThePriorityOfTheRule(int id)
        {
            try
            {
                var rog = _appDbContext.RuleOfGifts.SingleOrDefault(x => x.Id == id);
                var beforerog = _appDbContext.RuleOfGifts.SingleOrDefault(x=>x.Priority==rog.Priority-1);
                var temp = rog.Priority;
                if (beforerog == null)
                {
                    return new RuleOfGiftResponse { Success = false, Message = "Don't have previous priority " };

                }
                else
                {
                    rog.Priority = beforerog.Priority;
                    beforerog.Priority = temp;
                    _appDbContext.Update(rog);
                    _appDbContext.Update(beforerog);
                    _appDbContext.SaveChanges();
                    return new RuleOfGiftResponse { Success = true, Message = "The priority of the rule " + rog.Name + " is raised." };

                }
            }
            catch
            {
                return new RuleOfGiftResponse { Success = false, Message = "error" };
            }

        }

        public async Task<RuleOfGiftResponse> ReduceThePriorityOfTheRule(int id)
        {
            try
            {
                var rog = _appDbContext.RuleOfGifts.SingleOrDefault(x => x.Id == id);
                var afterrog = _appDbContext.RuleOfGifts.SingleOrDefault(x => x.Priority == rog.Priority + 1);
                var temp = rog.Priority;
                if (afterrog == null)
                {
                    return new RuleOfGiftResponse { Success = false, Message = "Don't have next priority " };

                }
                else
                {
                    rog.Priority = afterrog.Priority;
                    afterrog.Priority = temp;
                    _appDbContext.Update(rog);
                    _appDbContext.Update(afterrog);
                    _appDbContext.SaveChanges();
                    return new RuleOfGiftResponse { Success = true, Message = "The priority of the rule " + rog.Name + " is reduced." };

                }

                rog.Priority = afterrog.Priority;
                afterrog.Priority = temp;
                _appDbContext.Update(rog);
                _appDbContext.Update(afterrog);
                _appDbContext.SaveChanges();
                return new RuleOfGiftResponse { Success = true, Message = "The priority of the rule " + rog.Name + " is reduced." };
            }
            catch
            {
                return new RuleOfGiftResponse { Success = false, Message = "error" };
            }
        }

        public async Task<RuleOfGiftResponse> DeleteRuleOfGift(int id)
        {
            try
            {
                var rog = _appDbContext.RuleOfGifts.SingleOrDefault(x => x.Id == id);
                _appDbContext.Remove(rog);
                _appDbContext.SaveChanges();
                var listrog = _appDbContext.RuleOfGifts.OrderBy(x => x.Priority).ToList();
                int i = 1;
                foreach(var r in listrog)
                {
                    r.Priority = i;
                    i++;
                    _appDbContext.Update(r);
                }
                _appDbContext.SaveChanges();
                 return new RuleOfGiftResponse { Success = true, Message = "The rule "+rog.Name+" is deleted" };
            }
            catch
            {
                return new RuleOfGiftResponse { Success = false, Message = "error" };
            }
            
        }

        public ICollection<WinnersVM> GetAllWinners()
        {
            var allwinner = _appDbContext.Winners.ToList();
            var listwinnerVM = new List<WinnersVM>();
            foreach (var winner in allwinner)
            {
                var ctm = _appDbContext.Customers.SingleOrDefault(x => x.Id == winner.IdCustomer);
                var g = _appDbContext.Gifts.SingleOrDefault(x => x.Id == winner.IdGift);
                var gc = _appDbContext.GiftCategories.SingleOrDefault(x => x.Id == g.IdGiftCategory);
                WinnersVM wn = new WinnersVM
                {
                    FullName = ctm.Name,
                    WinDate = winner.WinDate,
                    GiftCode = g.GiftCode,
                    GiftName = gc.Name,
                    SentGift = winner.SendGiftStatus
                };
                listwinnerVM.Add(wn);
            }
            return listwinnerVM;
        }
    }
}
    