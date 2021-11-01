using Domain.Enums;
using Domain.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Models
{
    public class Usuario : BaseModel
    {
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Token { get; private set; }
        public RoleEnum Role { get; private set; }
        public Guid ColaboradorID { get; private set; }
        public Colaborador Colaborador { get; private set; }

        public void Cadastrar(string cpf, Guid colaboradorID, RoleEnum role)
        {
            Login = cpf;
            Senha = PasswordUtil.RandomPassword(8);
            Token = GerarTokenPrimeiroAcesso();
            Role = role;
            ColaboradorID = colaboradorID;
        }

        public void EquipararPropriedades(string cpf, RoleEnum role)
        {
            Login = cpf;
            Role = role;            
        }

        // Private Methods
        private string GerarTokenPrimeiroAcesso()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("RandomPassword", Senha));

            var audience = Environment.GetEnvironmentVariable("AUDIENCE");
            var issuer = Environment.GetEnvironmentVariable("ISSUER");

            DateTime tokenDate = DateTime.UtcNow;
            DateTime expiry = tokenDate + TimeSpan.FromDays(30);
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
