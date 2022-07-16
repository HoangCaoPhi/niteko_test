using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface ICustomerBL
    {
        /// <summary>
        /// Lấy tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        Task<List<Customer>> GetAll();
    }
}
