using CMS_1.Models;
using CMS_1.Models.Campaign;
using CMS_1.System.Campaign;
using System.Drawing;
using BarcodeLib;
using System.Drawing.Imaging;
using QRCoder;
using SkiaSharp;
using CMS_1.Models.Campaigns;
using CMS_1.Models.Filters;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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

        public ICollection<CampaignMV> GetAllCampaigns()
        {
            var ds =  _appDbContext.Campaigns.ToList();
            var listcpVM = new List<CampaignMV>();
            foreach( var cp in ds)
            {
                var countcode = _appDbContext.Barcodes.Count(x => x.IdCampaign == cp.Id);
                var countscanned = _appDbContext.Barcodes.Where(x=>x.IsScanned==true).Count(x => x.IdCampaign == cp.Id);
                var countgift = _appDbContext.Gifts.Count(x => x.IdCampaign == cp.Id);
                var cpVM = new CampaignMV
                {
                    Id = cp.Id,
                    Name = cp.Name,
                    StartDate = cp.StartDay,
                    EndDate = cp.EndDay,
                    ActiveCode = countcode,
                    GiftQuantity = countgift,
                    Scanned = countscanned,
                    UseForSpin = 0,
                    Win = 0
                };
                listcpVM.Add(cpVM);
            }
            return listcpVM;
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
                    IsScanned = barcode.IsScanned,
                    Active = barcode.Active,
                    campaign = cp.Name
                };
                listbarcode.Add(barcodeVM);
            }
            return listbarcode;
        }
        public ICollection<BarcodeHistoryVM> GetAllBarcodeHistories(int id)
        {
            var allbarcode = _appDbContext.Barcodes.Where(x => x.IdCampaign == id).ToList();
            var cp = _appDbContext.Campaigns.SingleOrDefault(x => x.Id == id);
            var listbarcodehistory = new List<BarcodeHistoryVM>();
            foreach (var barcode in allbarcode)
            {
                BarcodeHistoryVM barcodehistory = new BarcodeHistoryVM
                {
                    Id = barcode.Id,
                    Code = barcode.Code,
                    CreateDate = barcode.CreateDate,
                    ScannedDate = barcode.ScannedDate,
                    SpinDate = barcode.SpinDate,
                    Scanned = barcode.IsScanned,
                    Owner = barcode.Owner,
                    UseForSpin = barcode.UseForSpin
                };
                listbarcodehistory.Add(barcodehistory);
            }
            return listbarcodehistory;
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
                GiftCampaignMV barcodeVM = new GiftCampaignMV
                {
                    Id = giftt.Id,
                    GiftCode = giftt.GiftCode,
                    CreateDate = giftt.CreateDate,
                    Usagelimit = giftt.UsageLimit,
                    Active = giftt.Active,
                    Used = giftt.Used,
                    GiftName = giftt.GiftName,
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
                WinnersVM wn = new WinnersVM
                {
                    FullName = ctm.Name,
                    WinDate = winner.WinDate,
                    GiftCode = g.GiftCode,
                    GiftName = g.GiftName,
                    SentGift = winner.SendGiftStatus
                };
                listwinnerVM.Add(wn);
            }
            return listwinnerVM;
        }
        
        public MemoryStream SpreadsheetBarcode(List<BarcodeVM> model)
        {
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage(stream))
            {
                var worksheet = pck.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells.LoadFromCollection(model, true);
                pck.Save();
            }
            stream.Position = 0;
            return stream;
        }
        public MemoryStream SpreadsheetBarcodeHistory(List<BarcodeHistoryVM> model)
        {
            var stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage(stream))
            {
                var worksheet = pck.Workbook.Worksheets.Add("Sheet2");
                worksheet.Cells.LoadFromCollection(model, true);
                pck.Save();
            }
            stream.Position = 0;
            return stream;
        }
        public MemoryStream SpreadsheetGift(List<GiftCampaignMV> model)
        {

            var stream = new MemoryStream();


            using (ExcelPackage pck = new ExcelPackage(stream))
            {
                var worksheet = pck.Workbook.Worksheets.Add("Sheet3");
                worksheet.Cells.LoadFromCollection(model, true);
                pck.Save();
            }
            stream.Position = 0;
            return stream;
        }
        public MemoryStream SpreadsheetWinner(List<WinnersVM> model)
        {

            var stream = new MemoryStream();


            using (ExcelPackage pck = new ExcelPackage(stream))
            {
                var worksheet = pck.Workbook.Worksheets.Add("Sheet4");
                worksheet.Cells.LoadFromCollection(model, true);
                pck.Save();
            }
            stream.Position = 0;
            return stream;
        }

        public ICollection<CampaignMV> FilterCampaign(bool MatchAllFilter, List<Condition_Filter> Conditions)
        {
            var str = "";
            for (int i = 0; i < Conditions.Count; i++)
            {
               
                if (i > 0)
                {
                    if (MatchAllFilter == true)
                    {
                        str = str + "AND ";
                    }
                    else
                    {
                        str = str + "OR ";
                    }
                }
                Condition_Filter condition = Conditions[i];
                str = str + SqlFilterCampaign(condition.SearchCriteria, condition.Condition, condition.Value);

            }

            var allcampaigns = _appDbContext.Campaigns.FromSqlRaw("Select * from dbo.Campaign Where " + str).ToList();
           
            var listCampaign = new List<CampaignMV>();
            foreach (var cp in allcampaigns)
            {
                var countwin = 0;
                var giftofcp =_appDbContext.Gifts.Where(x => x.IdCampaign == cp.Id).ToList();
                foreach(var gift in giftofcp)
                {
                    countwin = _appDbContext.Winners.Where(x => x.IdGift == gift.Id).Count();
                }
                CampaignMV campaignVM = new CampaignMV
                {
                    Id = cp.Id,
                    Name = cp.Name,
                    StartDate = cp.StartDay,
                    EndDate = cp.EndDay,
                    ActiveCode = _appDbContext.Barcodes.Where(x=>x.IdCampaign==cp.Id).Count(),
                    GiftQuantity = _appDbContext.Gifts.Where(x => x.IdCampaign == cp.Id).Count(),
                    Scanned = _appDbContext.Barcodes.Where(x => x.IdCampaign == cp.Id).Count(x=>x.IsScanned==true),
                    UseForSpin = _appDbContext.Barcodes.Where(x => x.IdCampaign == cp.Id).Count(x => x.UseForSpin == true),
                    Win = countwin
                };
                listCampaign.Add(campaignVM);
            }
            return listCampaign;
        }
        public ICollection<BarcodeVM> FilterBarcode(bool MatchAllFilter, List<Condition_Filter> Conditions)
        {

            var str = "";
            for (int i = 0; i < Conditions.Count; i++)
            {

                if (i > 0)
                {
                    if (MatchAllFilter == true)
                    {
                        str = str + "AND ";
                    }
                    else
                    {
                        str = str + "OR ";
                    }
                }
                Condition_Filter condition = Conditions[i];
                str = str + SqlFilterBarcode(condition.SearchCriteria, condition.Condition, condition.Value);

            }

            var allbarcode = _appDbContext.Barcodes.FromSqlRaw("Select * from dbo.Barcode Where " + str).ToList();
            var listbarcode = new List<BarcodeVM>();
            foreach (var barcode in allbarcode)
            {
                var cp = _appDbContext.Campaigns.SingleOrDefault(x => x.Id == barcode.IdCampaign);
                BarcodeVM barcodeVM = new BarcodeVM
                {
                    Id = barcode.Id,
                    Code = barcode.Code,
                    BarCode = barcode.BarCode,
                    QRcode = barcode.QRcode,
                    CreateDate = barcode.CreateDate,
                    ExpiredDate = cp.EndDay,
                    ScannedDate = barcode.ScannedDate,
                    IsScanned = barcode.IsScanned,
                    Active = barcode.Active,
                    campaign = cp.Name
                };
                listbarcode.Add(barcodeVM);
            }
            return listbarcode;
        }
        public ICollection<BarcodeHistoryVM> FilterBarcodeHistory(bool MatchAllFilter, List<Condition_Filter> Conditions)
        {

            var str = "";
            for (int i = 0; i < Conditions.Count; i++)
            {

                if (i > 0)
                {
                    if (MatchAllFilter == true)
                    {
                        str = str + "AND ";
                    }
                    else
                    {
                        str = str + "OR ";
                    }
                }
                Condition_Filter condition = Conditions[i];
                str = str + SqlFilterBarcode(condition.SearchCriteria, condition.Condition, condition.Value);

            }

            var allbarcode = _appDbContext.Barcodes.FromSqlRaw("Select * from dbo.Barcode Where " + str).ToList();
            var listbarcodehistory = new List<BarcodeHistoryVM>();
            foreach (var barcode in allbarcode)
            {
                BarcodeHistoryVM barcodehistory = new BarcodeHistoryVM
                {
                    Id = barcode.Id,
                    Code = barcode.Code,
                    CreateDate = barcode.CreateDate,
                    ScannedDate = barcode.ScannedDate,
                    SpinDate = barcode.SpinDate,
                    Scanned = barcode.IsScanned,
                    Owner = barcode.Owner,
                    UseForSpin = barcode.UseForSpin
                };
                listbarcodehistory.Add(barcodehistory);
            }
            return listbarcodehistory;
        }
        public ICollection<GiftCampaignMV> FilterGift(bool MatchAllFilter, List<Condition_Filter> Conditions)
        {
            var str = "";
            for (int i = 0; i < Conditions.Count; i++)
            {

                if (i > 0)
                {
                    if (MatchAllFilter == true)
                    {
                        str = str + "AND ";
                    }
                    else
                    {
                        str = str + "OR ";
                    }
                }
                Condition_Filter condition = Conditions[i];
                str = str + SqlFilterGift(condition.SearchCriteria, condition.Condition, condition.Value);

            }

            var allgift = _appDbContext.Gifts.FromSqlRaw("Select * from dbo.Gift Where " + str).ToList();
            var listgift = new List<GiftCampaignMV>();
            foreach (var g in allgift)
            {
                GiftCampaignMV gift = new GiftCampaignMV
                {
                    Id = g.Id,
                    GiftCode = g.GiftCode,
                    GiftName = g.GiftName,
                    CreateDate = g.CreateDate,
                    Usagelimit = g.UsageLimit,
                    Active = g.Active,
                    Used = g.Used,
                };
                listgift.Add(gift);
            }
            return listgift;
        }
        public ICollection<WinnersVM> FilterWinner(bool MatchAllFilter, List<Condition_Filter> Conditions)
        {
            var str = "";
            for (int i = 0; i < Conditions.Count; i++)
            {

                if (i > 0)
                {
                    if (MatchAllFilter == true)
                    {
                        str = str + "AND ";
                    }
                    else
                    {
                        str = str + "OR ";
                    }
                }
                Condition_Filter condition = Conditions[i];
                str = str + SqlFilterWinner(condition.SearchCriteria, condition.Condition, condition.Value);

            }

            var allwinner = _appDbContext.Winners.FromSqlRaw("Select w.id, w.WinDate, w.SendGiftStatus, w.IdCustomer, w.IdGift from dbo.Winner w , dbo.Gift g , dbo.Customer c Where w.IdCustomer = c.id and w.IdGift = g.id and " + str).ToList();
            var listwinners = new List<WinnersVM>();
            foreach (var winner in allwinner)
            {
                var ctm = _appDbContext.Customers.SingleOrDefault(x => x.Id == winner.IdCustomer);
                var g = _appDbContext.Gifts.SingleOrDefault(x => x.Id == winner.IdGift);
                WinnersVM wn = new WinnersVM
                {
                    FullName = ctm.Name,
                    WinDate = winner.WinDate,
                    GiftCode = g.GiftCode,
                    GiftName = g.GiftName,
                    SentGift = winner.SendGiftStatus
                };
                listwinners.Add(wn);
            }
            return listwinners;
        }

        private string SqlFilterCampaign(int SearchCriteria, int Condition, string Value)
        {
            var str = "";
            if (SearchCriteria == 1)
            {
                str = str + "name ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 2)
            {
                str = str + "StartDay ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }
            if (SearchCriteria == 3)
            {
                str = str + "EndDay ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }

            return str;
        }
        private string SqlFilterBarcode(int SearchCriteria, int Condition, string Value)
        {
            var str = "";
            if (SearchCriteria == 1)
            {
                str = str + "Code ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 2)
            {
                str = str + "CreateDate ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }
            if (SearchCriteria == 3)
            {
                str = str + "ExpiredDate ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }
            if (SearchCriteria == 4)
            {
                str = str + "ScannedDate ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }
            if (SearchCriteria == 5)
            {
                str = str + "IsScanned ";
                if (Condition == 1)
                {
                    return str + "= '" + Value + "' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "= '" + Value + "' ";
                }
            }
            if (SearchCriteria == 6)
            {
                str = str + "Active ";
                if (Condition == 1)
                {
                    return str + "= '" + Value + "' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "= '" + Value + "' ";
                }
            }
            return str;
        }
        private string SqlFilterBarcodeHistory(int SearchCriteria, int Condition, string Value)
        {
            var str = "";
            if (SearchCriteria == 1)
            {
                str = str + "Code ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 2)
            {
                str = str + "CreateDate ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }
            
            if (SearchCriteria == 3)
            {
                str = str + "ScannedDate ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }
            if (SearchCriteria == 4)
            {
                str = str + "SpinDate ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }
            if (SearchCriteria == 5)
            {
                str = str + "Owner ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 6)
            {
                str = str + "IsScanned ";
                if (Condition == 1)
                {
                    return str + "= '" + Value + "' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "= '" + Value + "' ";
                }
            }
            if (SearchCriteria == 7)
            {
                str = str + "UseForSpin ";
                if (Condition == 1)
                {
                    return str + "= '" + Value + "' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "= '" + Value + "' ";
                }
            }
            return str;
        }
        private string SqlFilterGift(int SearchCriteria, int Condition, string Value)
        {
            var str = "";
            if (SearchCriteria == 1)
            {
                str = str + "GiftCode ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 2)
            {
                str = str + "GiftName ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 3)
            {
                str = str + "CreateDate ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }

            if (SearchCriteria == 4)
            {
                str = str + "UsageLimit ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }
            
            if (SearchCriteria == 5)
            {
                str = str + "Active ";
                if (Condition == 1)
                {
                    return str + "= '" + Value + "' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "= '" + Value + "' ";
                }
            }
            return str;
        }
        private string SqlFilterWinner(int SearchCriteria, int Condition, string Value)
        {
            var str = "";
            if (SearchCriteria == 1)
            {
                str = str + "c.Name ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 2)
            {
                str = str + "w.WinDate ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }
            if (SearchCriteria == 3)
            {
                str = str + "g.GiftCode ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            

            if (SearchCriteria == 4)
            {
                str = str + "g.GiftName ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }

            if (SearchCriteria == 5)
            {
                str = str + "w.SendGiftStatus ";
                if (Condition == 1)
                {
                    return str + "= '" + Value + "' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "= '" + Value + "' ";
                }
            }
            return str;
        }

        
    }
}
    