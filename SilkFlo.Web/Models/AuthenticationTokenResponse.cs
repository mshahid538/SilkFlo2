namespace SilkFlo.Web.Models
{
    public class AuthenticationTokenResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
    }
}
