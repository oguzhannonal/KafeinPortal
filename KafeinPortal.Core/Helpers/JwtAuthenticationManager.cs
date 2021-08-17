using KafeinPortal.Data.Model.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace KafeinPortal.Core.Helpers
{

    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string _key;
        
        public JwtAuthenticationManager(string key)
        {
            _key = key;
        }
        public string Authenticate(UserLogin user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject =  new ClaimsIdentity(new Claim[]{ 
                    new Claim(ClaimTypes.Name,user.username)
                    }),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                    )
               
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }





    }
}
