using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Services.Options
{
    public class JwtConfiguration
    {
        public string ISSUER { get; set; } // издатель токена
        public string AUDIENCE { get; set; } // потребитель токена
        public string KEY { get; set; }   // ключ для шифрации
        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
