using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IBA.Task3.Services
{
    public class AuthOptions
    {
        public const string ISSUER = "Server"; // издатель токена
        public const string AUDIENCE = "Client"; // потребитель токена
        const string KEY = "Karl_stole_corals_from_Clara"; // ключ для шифрации
        public const int LIFETIME = 10; // время жизни токена - 10 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}