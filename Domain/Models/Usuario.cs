using Domain.Enums;
using System;
using System.Security.Claims;
using Domain.Security;

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
            Role = role;
            ColaboradorID = colaboradorID;

            var randomCode = PasswordUtil.RandomPassword(8);
            var claim = new Claim("RandomCode", randomCode);
            Token = TokenUtil.GerarTokenJWT(30, claim);
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
            var randomCode = PasswordUtil.RandomPassword(8);
            var claim = new Claim("RandomCode", randomCode);
            Token = TokenUtil.GerarTokenJWT(2, claim);
            return Token;
        }

        public string TokenAcesso()
        {
            var claim = new Claim(ClaimTypes.Role, Role.GetDescription());
            return TokenUtil.GerarTokenJWT(5, claim);
        }
    }
}
