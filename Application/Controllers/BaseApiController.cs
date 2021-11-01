using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Application.Controllers
{
    //[Authorize(Policy = "Bearer")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public Guid? UsuarioLogadoID
        {
            get
            {
                return Guid.Parse("09116C41-6D15-4788-8931-9B8C3F5A805F");

                var userIdentity = User.Identity as ClaimsIdentity;
                var idClaim = userIdentity.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Sid);

                if (idClaim != null && idClaim.Value != null)
                {
                    Guid userId;
                    if (Guid.TryParse(idClaim.Value, out userId))
                    {
                        return userId;
                    }
                }
                return null;
            }
        }
    }
}