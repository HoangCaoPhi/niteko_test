using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface IOrderBL
    {
        /// <summary>
        /// Lấy tất cả chuyên mục
        /// </summary>
        /// <returns></returns>
        Task<List<Order>> GetAll();
        /// <summary>
        /// Lưu đơn đặt hàng
        /// </summary>
        /// <returns></returns>
        Task<ServiceResponse> Save(Order order);

        /// <summary>
        /// Tìm kiếm chuyên mục
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        Task<ServiceResponse> SearchCategory(Dictionary<string, string> categoryName);
    }
}
