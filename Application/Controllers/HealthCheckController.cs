using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Application.Controllers
{
    [AllowAnonymous]
    [Route("")]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            return Ok(string.Format("Ferramentaria API v.{0}", fvi.FileVersion));
        }
    }
}