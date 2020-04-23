using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace carRental_WebAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _jwtKey;

        public AuthRepository(IConfiguration config)
        {
            _jwtKey = config["TokenKey"];
        }

        public string CreateToken(string userName)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName)
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)), SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool Login(string userName, string password)
        {
            return (userName == "admin" && password == "Altomobile");
        }
    }
}
