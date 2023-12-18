using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JWT.Builder;
using System.Security.Claims;
using JWT.Algorithms;

namespace WebApplication6.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        const string UserOK = "user1";
        const string passwordOK = "pass";
        [HttpGet]
        public async Task<IActionResult> Login([FromQuery]string user,[FromQuery]string password)
        {
            if(user==UserOK && password==passwordOK)
            {
                string JWTToken = JwtBuilder.Create()
                    .AddClaim(ClaimTypes.Name, "abbas hasanzadeh")
                    .AddClaim("Channel", "CodeEasyMS")
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
                    .Encode();
                return Ok(JWTToken);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
