using BL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Threading.Tasks;

namespace Niteko.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticate _authen;
        public AuthenticateController(IAuthenticate authen)
        {
            _authen = authen;
        }

        [HttpPost("Login")]
        public async Task<ServiceResponse> Login([FromBody] Customer customer)
        {
            return await _authen.Login(customer, Response);
        }
    }
}
