using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System.Security.Claims;

namespace Api.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiContentController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [HttpGet("Private")]
        public IActionResult Private()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return BadRequest();
            }

            return Content($"You have authorized access to resources belonging to {identity.Name} on Zirku.Api1.");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            var qualquerCoisa = HttpContext.Request.Headers;
            return Content("This is a public endpoint that is at Zirku.Api1; it does not require authorization.");
        }
    }
}
