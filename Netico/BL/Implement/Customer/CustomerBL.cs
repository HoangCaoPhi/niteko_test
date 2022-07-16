using BL.Interface;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Implement
{

    public class CustomerBL : BaseBL, ICustomerBL
    {
        protected readonly IDatabaseServiceBL _databaseService;

        public CustomerBL(IDatabaseServiceBL databaseService) : base(databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Lấy tất cả người dùng
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customer>> GetAll()
        {
            var listCategory = await _databaseService.QueryStoreProcedure<Customer>("niteko", "Proc_Customer_GetAll");
            return listCategory;
        }
    }
}
