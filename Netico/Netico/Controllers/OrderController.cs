using BL;
using BL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderBL _OrderBL;
        private readonly ILogService _logService;
        public OrderController(IOrderBL OrderBL, ILogService logService)
        {
            _OrderBL = OrderBL;
            _logService = logService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ServiceResponse> GetOrder()
        {
            try
            {
                var res = new ServiceResponse();
                res.Data = await _OrderBL.GetAll();
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

        [HttpPost]
        [Route("Save")]
        public async Task<ServiceResponse> SaveOrder([FromBody] Order order)
        {
            try
            {
                var res = await _OrderBL.Save(order);
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

        [HttpPost]
        [Route("SearchCategory")]
        public async Task<ServiceResponse> SearchCategory([FromBody] Dictionary<string, string> categoryName)
        {
            try
            {
                var res = await _OrderBL.SearchCategory(categoryName);
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
