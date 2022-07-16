using BL;
using BL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Threading.Tasks;

namespace Netico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerBL _CustomerBL;
        private readonly ILogService _logService;
        public CustomerController(ICustomerBL CustomerBL, ILogService logService)
        {
            _CustomerBL = CustomerBL;
            _logService = logService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ServiceResponse> GetCustomer()
        {
            try
            {
                var res = new ServiceResponse();
                res.Data = await _CustomerBL.GetAll();
                res.Subcode = 200;
                return res;
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Có lỗi xảy ra");
                return new ServiceResponse()
                {
                    Success = false
                };
            }
        }
    }
}
