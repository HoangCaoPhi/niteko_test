using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProductBL : BaseBL, IProductBL
    {
        protected readonly IDatabaseServiceBL _databaseService;

        public ProductBL(IDatabaseServiceBL databaseService) : base(databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Lấy tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetAll()
        {
            var listCategory = await _databaseService.QueryStoreProcedure<Product>("niteko", "Proc_Product_GetAll");
            return listCategory;
        }
    }
}
