using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BL
{
    public interface IDatabaseServiceBL
    {
        /// <summary>
        /// Lấy ra connection
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection(string appCode);

        /// <summary>
        /// Lấy dữ liệu khi với commantext
        /// </summary>
        /// <typeparam name="T">Trả về danh sách dạng list</typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParam"></param>
        /// <param name="modelType"></param>
        /// <param name="transaction"></param>
        /// <param name="connection">Connection thực thi</param>
        /// <returns></returns>
        /// HCPHI 
        Task<List<T>> QueryCommandText<T>(string appCode,
                                          string commandText,
                                          Dictionary<string, object> dicParam = null,
                                          IDbTransaction transaction = null,
                                          IDbConnection connection = null
        );

        /// <summary>
        /// Lấy dữ liệu DB với Store Procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appCode"></param>
        /// <param name="storeProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        /// HCPHI
        Task<List<T>> QueryStoreProcedure<T>(string appCode,
                                             string storeProcedureName,
                                             object param = null,
                                             IDbTransaction transaction = null,
                                             IDbConnection connection = null
        );

        /// <summary>
        /// Thực thi trả về true false
        /// </summary>
        /// <param name="appCode"></param>
        /// <param name="storeProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        Task<bool> ExecuteUsingStore(string appCode,
                                     string storeProcedureName,
                                     object param = null,
                                     IDbTransaction transaction = null,
                                     IDbConnection connection = null);
    }
}
