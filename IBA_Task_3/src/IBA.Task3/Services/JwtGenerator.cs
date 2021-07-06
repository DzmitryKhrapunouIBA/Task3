using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IBA.Task3.DAL.Models;
using IBA.Task3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IBA.Task3
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly SymmetricSecurityKey _key;

        private TimeSpan _expires;
        private string _issuer;
        private string _audience;

        public JwtGenerator(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("Authentication:TokenValidationParameters:SecretKey")));
            _expires = config.GetValue<TimeSpan>("Authentication:Expires");
            _issuer = config.GetValue<string>("Authentication:TokenValidationParameters:ValidIssuer");
            _audience = config.GetValue<string>("Authentication:TokenValidationParameters:ValidAudience");
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim("UserId", user.Id.ToString())
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _issuer,
                Audience = _audience,
                Subject = new ClaimsIdentity(claims),
                Expires = now.Add(_expires),
                SigningCredentials = credentials,
                NotBefore = now
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}