using BL.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implement
{
    public class OrderBL : BaseBL, IOrderBL
    {
        protected readonly IDatabaseServiceBL _databaseService;

        public OrderBL(IDatabaseServiceBL databaseService) : base(databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Lấy tất cả người dùng
        /// </summary>
        /// <returns></returns>
        public async Task<List<Order>> GetAll()
        {
            var listCategory = await _databaseService.QueryStoreProcedure<Order>("niteko", "Proc_Order_GetAll");
            return listCategory;
        }

        /// <summary>
        /// Lưu đơn đặt hàng
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> Save(Order order)
        {
            var res = new ServiceResponse();
            var success = await _databaseService.ExecuteUsingStore("niteko", "Proc_Order_Insert", order);
            if(success)
            {
                res.Data = order;
                res.Subcode = 200;
            }
            return res;
        }

        /// <summary>
        /// Tìm kiếm theo chuyên mục
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> SearchCategory(Dictionary<string, string> dicCategory)
        {
            var categoryName = dicCategory["CategoryName"];
            var res = new ServiceResponse();
            string CategoryName = "%" + categoryName + "%";
            var sql = @"SELECT * FROM  `order` o WHERE o.CategoryName LIKE @CategoryName";

            var param = new Dictionary<string, object>
            {
                { "CategoryName", categoryName}
            };
            var result = await _databaseService.QueryCommandText<Order>("niteko", sql, param);
            if(result != null)
            {
                res.Data = result;
                res.Subcode = 200;
            }
            return res;
        }
    }
}
