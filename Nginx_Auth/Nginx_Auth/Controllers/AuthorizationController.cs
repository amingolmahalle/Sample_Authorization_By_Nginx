using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Nginx_Auth.Entity;
using Nginx_Auth.Services;

namespace Nginx_Auth.Controllers
{
    public class Authorization : Controller
    {
        private readonly IAuthorizationServices _authorizationServices;

        public Authorization(IAuthorizationServices authorizationServices)
        {
            _authorizationServices = authorizationServices;
        }

        [HttpGet("getToken")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromQuery] string username, [FromQuery] string password)
        {
            User user = await _authorizationServices.AuthorizeAsync(username, password);

            if (user != null)
                return Ok(await _authorizationServices.GenerateTokenAsync());

            return Unauthorized();
        }

        [HttpGet("isValidToken")]
        public async Task<IActionResult> IsValidToken()
        {
            string token;

            if (HttpContext.Request.Headers.TryGetValue("token", out StringValues tokenValues))
            {
                token = tokenValues.FirstOrDefault()?.Trim();

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }

            User user = await _authorizationServices.DeserializeToken(token);

            if (user == null)
                return Unauthorized();

            HttpContext.Response.Headers.Add("user-id", user.Id.ToString());
            HttpContext.Response.Headers.Add("fullname", $"{user.FirstName} {user.LastName}");

            return Ok();
        }
    }
}