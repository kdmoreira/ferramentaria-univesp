using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Security
{
    public static class TokenUtil
    {
        public static string GerarTokenJWT(int prazoExpiracaoDias, Claim claim)
        {
            var claims = new List<Claim>();
            claims.Add(claim);

            var audience = Environment.GetEnvironmentVariable("AUDIENCE");
            var issuer = Environment.GetEnvironmentVariable("ISSUER");

            DateTime tokenDate = DateTime.UtcNow;
            DateTime expiry = tokenDate + TimeSpan.FromDays(prazoExpiracaoDias);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(Environment.GetEnvironmentVariable("KEYSEC")));
            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: expiry,
                signingCredentials: credentials,
                claims: claims,
                notBefore: tokenDate
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
