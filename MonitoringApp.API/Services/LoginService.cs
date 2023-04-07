using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using MonitoringApp.API.IServices;
using MonitoringApp.Data;
using MonitoringApp.Model.RequestResponseClasses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace MonitoringApp.API.Services
{
    public class LoginService : ILoginService
    {
        private MonitoringAppDbContext _dbContext;

        public LoginService(MonitoringAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public LoginResponse Login(LoginRequest req)
        {
            LoginResponse resp = new LoginResponse();

            if (string.IsNullOrEmpty(req.AccountName))
            {
                resp.isSuccess = false;
                resp.ErrorMessage = "Kullanıcı Adınızı Giriniz.";
            }
            else
            {
                var hashedPassword = CreatePasswordHash(req.Password);
                var user = _dbContext.Users.Where(a => a.AccountName == req.AccountName && a.HashPassword == hashedPassword).FirstOrDefault();

                if (user == null)
                {
                    resp.isSuccess = false;
                    resp.ErrorMessage = "Kullanıcı aktif değil! Sistem yöneticinizle iletişime geçiniz";
                    return resp;
                }
                else
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes("bu benim secret key alanım");

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, req.AccountName));
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, user.UserId.ToString()));

                    var role = _dbContext.Roles.Find(user.RoleId);
                    if (role != null)
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claimsIdentity,
                        Expires = DateTime.UtcNow.AddHours(1), 

                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor); 
                    var tokenString = tokenHandler.WriteToken(token);

                    resp.isSuccess = true;
                    resp.UserName = user.AccountName;

                    resp.Token = tokenString;
                    resp.RoleId = user.RoleId;
                }
            }
            return resp;
        }

        private string CreatePasswordHash(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            var passBytes = provider.ComputeHash(encoding.GetBytes(password + "bu benim secret key alanım"));
            password = Convert.ToBase64String(passBytes);
            return password;
        }
    }
}
