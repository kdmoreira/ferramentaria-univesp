using Domain.Enums;
using System;
using System.Security.Claims;
using Domain.Security;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Usuario : BaseModel
    {
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Token { get; private set; }
        public RoleEnum Role { get; private set; }
        public Guid ColaboradorID { get; private set; }
        public Colaborador Colaborador { get; set; }

        public void Cadastrar(string cpf, Guid colaboradorID, RoleEnum role)
        {
            Login = cpf;
            Senha = PasswordUtil.RandomPassword(8);
            Role = role;
            ColaboradorID = colaboradorID;

            var claims = new List<Claim>();
            var randomCode = PasswordUtil.RandomPassword(8);
            var claim = new Claim("RandomCode", randomCode);
            claims.Add(claim);
            Token = TokenUtil.GerarTokenJWT(30, claims);
        }

        public void EquipararPropriedades(string cpf, RoleEnum role)
        {
            Login = cpf;
            Role = role;
        }

        public void TrocarSenha(string senha)
        {
            Senha = senha.CriptografarSenha();
            Token = null;
        }

        public string TokenRecuperacaoSenha()
        {
            var claims = new List<Claim>();
            var randomCode = PasswordUtil.RandomPassword(8);
            var claim = new Claim("RandomCode", randomCode);            
            claims.Add(claim);
            Token = TokenUtil.GerarTokenJWT(2, claims);
            return Token;
        }

        public string TokenAcesso()
        {
            var claims = new List<Claim>();
            var claimRole = new Claim(ClaimTypes.Role, Role.GetDescription());
            var claimID = new Claim(ClaimTypes.PrimaryGroupSid, ID.ToString());
            claims.Add(claimRole);
            claims.Add(claimID);
            return TokenUtil.GerarTokenJWT(5, claims);
        }
    }
}
