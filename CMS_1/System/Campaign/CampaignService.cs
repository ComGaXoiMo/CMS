using CMS_1.Models;
using CMS_1.Models.Campaign;
using CMS_1.System.Campaign;
using System.Drawing;
using BarcodeLib;
using System.Drawing.Imaging;
using QRCoder;
using SkiaSharp;

namespace CMS_1.System.Campaign
{
    public class CampaignService : ICampaignService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;
        

        public CampaignService(IConfiguration configuration, AppDbContext appDbContext)
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
                    CodeUsageLimit = model.CodeUsageLimit,
                    Unlimited = model.Unlimited,
                    CountCode = model.CountCode,
                    IdCharset = model.IdCharset,
                    CodeLength = model.CodeLength,
                    Prefix = model.Prefix,
                    Postfix = model.Postfix,
                    IdProgramSize = model.IdProgramSize,
                    StartDay = model.timeFrame.StartDay.Date,
                    StartTime = model.timeFrame.StartDay.TimeOfDay,
                    EndDay = model.timeFrame.EndDay.Date,
                    EndTime = model.timeFrame.EndDay.TimeOfDay,

                };
                _appDbContext.Add(campaign);
                _appDbContext.SaveChanges();
                var cp = _appDbContext.Campaigns.SingleOrDefault(x => x.Name == model.Name);
                for(int i = 0;i< model.CountCode; i++)
                {
                    AutoCreateBarCode(cp.Id, model.CodeLength, model.IdCharset, model.Prefix);
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
        
        public void AutoCreateBarCode(int idcampaign, int length, int idcharset, string prefix)
        {
            int postfix = length - prefix.Length;
            var barcodestring = new char[postfix];
            var random = new Random();
            var charset = GetTypeOfCharset(idcharset);
            for(int i =0; i< barcodestring.Length; i++)
            {
                barcodestring[i] = charset[random.Next(charset.Length)];
            }
            string finalpostfix = new string(barcodestring);
            var finalbarcode = new string(prefix + finalpostfix);
            CreateBarcodeAndQRcode(finalbarcode);
            var barcode = new Models.Barcode
            {
                IdCampaign = idcampaign,
                Code = finalbarcode,
                BarCode = new string(@"./Images/Barcode/" + finalbarcode + ".png"),
                QRcode = new string(@"./Images/QRcode/" + finalbarcode + ".png"),
                CreateDate = DateTime.Now,
                IsScanned = false,
                Owner = "",
                Active = true
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
            barcodeImage.Save(@"./Images/Barcode/" + character + ".png", ImageFormat.Png);
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData qrdata = qr.CreateQrCode(character, QRCodeGenerator.ECCLevel.L);
            QRCode qrCode = new QRCode(qrdata);
            var qrCodeAsBitmap = qrCode.GetGraphic(20);
            Image QRImage = qrCodeAsBitmap;
            QRImage.Save(@"./Images/QRcode/" + character + ".png", ImageFormat.Png);
            
        }
        public string GetTypeOfCharset(int type)
        {
            var decr = "";
            if (type == 1)
            {
                decr = "123456789";
            }
            else if (type ==2 )
            {
                decr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            else if (type == 3)
            {
                decr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            }
            return decr;
        }
    }
}
    