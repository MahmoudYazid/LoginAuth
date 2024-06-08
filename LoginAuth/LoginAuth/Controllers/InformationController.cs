using LoginAuth.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoginAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class InformationController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public UserModel GetCurrentUser() {

            var CurrentUser = HttpContext.User.Identity as ClaimsIdentity;

            if (CurrentUser != null) {
                var user = new UserModel
                {
                    Id = CurrentUser.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Name = CurrentUser.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = CurrentUser.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    ImageStr ="",
                    Password = ""
                };

                return user;

            }
            else
            {
                var userAlternative = new UserModel
                {
                    Id = "",
                    Name = "",
                    Email = "",
                    ImageStr = "",
                    Password = ""
                };

                return userAlternative;

            }
        
        }
    }
}
