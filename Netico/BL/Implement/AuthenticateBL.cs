using BL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace BL
{
    public class AuthenticateBL : BaseBL, IAuthenticate
    {
        private readonly IConfigService _config;
        protected readonly IDatabaseServiceBL _databaseService;

        public AuthenticateBL(IConfigService config, IDatabaseServiceBL databaseService) : base(databaseService)
        {
            _config = config;
            _databaseService = databaseService;
        }
        /// <summary>
        ///       Mã hóa mật khẩu với MD5
        /// </summary>
        /// <param name="input">
        ///       Đầu vào là một chuỗi mật khẩu của người dùng nhập vào
        /// </param>
        /// <returns>
        ///        Trả về một chuỗi đã được mã hóa
        /// </returns>
        public static string CreateMD5(string input)
        {
            // Lay du lieu de dung md5
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyen doi mang byte thanh chuoi hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        ///         Serivce xử lý login vào web
        /// </summary>
        /// <param name="userRequest">
        ///         Đầu vào là request của người dùng được gửi lên
        /// </param>
        /// <returns>
        ///         Trả về đối tượng AuthenticateResponse gồm thông tin người dùng và token nếu đúng và trả về null nếu sai
        /// </returns>
        public async Task<ServiceResponse> Login(Customer customer, HttpResponse httpResponse)
        {
            var res = new ServiceResponse();

            // Lấy thông tin email và password từ request
            var sql = @"SELECT * FROM customer c WHERE c.Email = @Email AND c.Password = @Password;";
            var param = new Dictionary<string, object>
            {
                { "Email", customer.Email},
                { "Password", customer.Password},
            };
            var result = await _databaseService.QueryCommandText<Customer>("niteko", sql, param);
            if (result is null) return new ServiceResponse();

            // Xử lý lưu session lên db
            var token = GenerateJwtToken(result.FirstOrDefault());
            var sessionID = Guid.NewGuid();
            var session = new Session()
            {
                SessionID = sessionID,
                LoginTime = DateTime.Now,
                ExpireTime = null,
                AccessToken = token
            };

            _= _databaseService.ExecuteUsingStore("niteko", "Proc_Session_Insert", session);
            AddCookie(httpResponse, "x_sessionID", sessionID.ToString(), null, "localhost", "/");          
            return new ServiceResponse()
            {
                Subcode = 200,
                Data = true
            };
        }

        /// <summary>
        /// Add Cookie để xử lý
        /// </summary>
        /// <param name="response"></param>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <param name="expireDate"></param>
        /// <param name="domain"></param>
        /// <param name="path"></param>
        public void AddCookie(HttpResponse response,
                              string cookieName,
                              string cookieValue,
                              DateTime? expireDate,
                              string domain = null,
                              string path = null)
        {
            var option = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };

            if (!string.IsNullOrEmpty(domain))
            {
                option.Domain = domain;
            }

            if (!string.IsNullOrEmpty(path))
            {
                option.Path = path;
            }

            if (expireDate.HasValue)
            {
                option.Expires = expireDate;
            }

            response.Cookies.Append(cookieName, cookieValue, option);
        }

        /// <summary>
        ///         SInh ra token để trả về cho người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        ///         Trả về token cho người dùng
        /// </returns>
        public string GenerateJwtToken(Customer customer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetJWT("Secret")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("CustomerID", customer.CustomerID.ToString()),
                new Claim("CustomerName", customer.Name.ToString())

            };

            var token = new JwtSecurityToken(
                issuer: _config.GetJWT("Issuer"),
                audience: _config.GetJWT("Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
