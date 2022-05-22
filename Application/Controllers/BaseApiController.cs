using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Application.Controllers
{
    [Authorize(Policy = "Bearer")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public Guid UsuarioLogadoID
        {
            get
            {
                return Guid.Parse("bb9ac2c8-c7d4-4c21-b6cf-84419b12a810");

                var userIdentity = User.Identity as ClaimsIdentity;
                var idClaim = userIdentity.Claims?
                    .FirstOrDefault(c => c.Type == ClaimTypes.PrimaryGroupSid);

                if (idClaim != null && idClaim.Value != null)
                {
                    Guid userId;
                    if (Guid.TryParse(idClaim.Value, out userId))
                    {
                        return userId;
                    }
                }
                return Guid.Empty;
            }
        }
    }
}