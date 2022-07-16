using BL.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implement
{
    public class CategoryBL : BaseBL, ICategoryBL
    {
        protected readonly IDatabaseServiceBL _databaseService;

        public CategoryBL(IDatabaseServiceBL databaseService) : base(databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Lấy tất cả chuyên mục
        /// </summary>
        /// <returns></returns>
        public async Task<List<Category>> GetAll()
        {
            var listCategory = await _databaseService.QueryStoreProcedure<Category>("niteko", "Proc_Category_GetAll");
            return listCategory;
        }
    }
}
