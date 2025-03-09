namespace ComercioCore.Application.Common
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpiryHours { get; set; }
    }
}
