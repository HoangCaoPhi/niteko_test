using BL;
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
    public class ProductController : Controller
    {
        private readonly IProductBL _ProductBL;
        private readonly ILogService _logService;
        public ProductController(IProductBL ProductBL, ILogService logService)
        {
            _ProductBL = ProductBL;
            _logService = logService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ServiceResponse> GetProduct()
        {
            try
            {
                var res = new ServiceResponse();
                res.Data = await _ProductBL.GetAll();
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
