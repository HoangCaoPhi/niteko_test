using BL;
using BL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Threading.Tasks;

namespace Netico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryBL _categoryBL;
        private readonly ILogService _logService;
        public CategoryController(ICategoryBL categoryBL, ILogService logService)
        {
            _categoryBL = categoryBL;
            _logService = logService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ServiceResponse> GetCategory()
        {
            try
            {
                var res = new ServiceResponse();
                res.Data = await _categoryBL.GetAll();
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
