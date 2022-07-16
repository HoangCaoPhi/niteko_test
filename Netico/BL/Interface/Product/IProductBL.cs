using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL
{
    public interface IProductBL
    {
        /// <summary>
        /// Lấy tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetAll();
    }
}
