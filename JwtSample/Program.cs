using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtSample
{
    internal class Program
    {
        private string secret = "superSecretKeyValue";
        static void Main(string[] args)
        {
            Console.WriteLine("Enter user name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter user password: ");
            string userPassword = Console.ReadLine();

            UserService userService = new();
            string token = userService.Authenticate(userName,
            userPassword);
            Console.WriteLine(token);
            Console.ReadKey(true);
        }


        internal class UserService
        {

            private const string secret = "kYp3s6v9y/B?E(H+";

            private IDictionary<string, string> _users = new Dictionary<string, string>()
        {
            {"root1", "test"}, // 0
            {"root2", "test"}, // 1
            {"root3", "test"}, // 2
            {"root4", "test"}  // 3
        };

            public string Authenticate(string user, string password)
            {
                if (string.IsNullOrWhiteSpace(user) ||
                    string.IsNullOrWhiteSpace(password))
                {
                    return string.Empty;
                }

                int i = 0;
                foreach (KeyValuePair<string, string> pair in _users)
                {
                    if (string.CompareOrdinal(pair.Key, user) == 0 &&
                    string.CompareOrdinal(pair.Value, password) == 0)
                    {

                        return GenerateToken(i);

                    }

                    i++;

                }

                return String.Empty;

            }

            private string GenerateToken(int id)
            {
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(secret);


                SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
                securityTokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(15);
                securityTokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
                securityTokenDescriptor.Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            });

                SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                return jwtSecurityTokenHandler.WriteToken(securityToken);
            }
            private string GeneratetToken(int id)
            {
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
                byte[] key = Encoding.ASCII.GetBytes(secret);

                SecurityTokenDescriptor securityTokenDescriptor = new();
                securityTokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(5);
                securityTokenDescriptor.SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
                securityTokenDescriptor.Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString())
                });
                
                var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                return jwtSecurityTokenHandler.WriteToken(securityToken);

            }


        }

    }
}