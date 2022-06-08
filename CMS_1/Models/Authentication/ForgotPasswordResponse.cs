namespace CMS_1.Models
{
    public class ForgotPasswordResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
