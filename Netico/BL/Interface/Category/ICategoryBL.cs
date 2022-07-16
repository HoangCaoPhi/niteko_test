using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface ICategoryBL
    {
        /// <summary>
        /// Lấy tất cả chuyên mục
        /// </summary>
        /// <returns></returns>
        Task<List<Category>> GetAll();
    }
}
