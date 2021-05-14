using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Nginx_HelloService.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet("/restricted/sayHello")]
        public IActionResult SayHello()
        {
            if (!HttpContext.Request.Headers.ContainsKey("fullname") ||
                !HttpContext.Request.Headers.ContainsKey("user-id"))
            {
                return Unauthorized();
            }

            string userId = HttpContext
                .Request
                .Headers.FirstOrDefault(h => h.Key == "user-id")
                .Value;
            
            string fullname = HttpContext
                .Request
                .Headers
                .FirstOrDefault(h => h.Key == "fullname")
                .Value;

            return Ok($"hello {fullname} with number: {userId} .");
        }

        [HttpGet("/public/healthy")]
        public IActionResult Healthy()
        {
            return Ok("I'm Healthy!");
        }
    }
}