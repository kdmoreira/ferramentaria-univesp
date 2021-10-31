using Microsoft.IdentityModel.Tokens;
using System;

namespace Domain.Security
{
    public class SigningConfiguration
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfiguration()
        {
            string secEnviromment = Environment.GetEnvironmentVariable("KEYSEC");

            Key = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secEnviromment));
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
