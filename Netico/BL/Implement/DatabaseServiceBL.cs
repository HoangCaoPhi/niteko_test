using BL.Interface;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BL
{
    public class DatabaseServiceBL : IDatabaseServiceBL
    {
        private readonly IConfigService _configService;
        public DatabaseServiceBL(IConfigService configService)
        {
            _configService = configService;
        }

        /// <summary>
        /// Thực thi 1 store trả về true false
        /// </summary>
        /// <param name="appCode"></param>
        /// <param name="storeProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteUsingStore(string appCode, string storeProcedureName, object param = null, IDbTransaction transaction = null, IDbConnection connection = null)
        {
            try
            {
                var cd = new CommandDefinition(storeProcedureName, commandType: CommandType.StoredProcedure, parameters: param, transaction: transaction);

                var con = (transaction != null ? transaction.Connection : connection);

                if (con != null)
                {
                    var query = await con.ExecuteAsync(cd);
                    if (query > 0) return true;
                }

                using (var cnn = GetConnection(appCode))
                {
                    var query = await cnn.ExecuteAsync(cd);
                    if (query > 0) return true;
                }
            }
            catch (Exception ex)
            {
                // Ghi log
                throw;
            }
            return false;
        }

        /// <summary>
        /// Lấy đối tượng conection
        /// </summary>
        /// <returns></returns>
        /// HCPHI
        public IDbConnection GetConnection(string appCode)
        {
            string connectionString = _configService.GetConnectionString(appCode);
            return new MySqlConnection(connectionString);
        }

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
        public async Task<List<T>> QueryCommandText<T>(string appCode,
                                                       string commandText,
                                                       Dictionary<string, object> dicParam,
                                                       IDbTransaction transaction = null,
                                                       IDbConnection connection = null
        )
        {
            try
            {
                List<T> result = new List<T>();
                var cd = new CommandDefinition(commandText, commandType: CommandType.Text, parameters: dicParam, transaction: transaction);

                var con = (transaction != null ? transaction.Connection : connection);

                if (con != null)
                {
                    var query = await con.QueryAsync<T>(cd);
                    result = query.ToList();
                    return result;
                }

                using (var cnn = GetConnection(appCode))
                {
                    var query = await cnn.QueryAsync<T>(cd);
                    result = query.ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Ghi log
                throw;
            }
        }

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
        public async Task<List<T>> QueryStoreProcedure<T>(string appCode,
                                                    string storeProcedureName,
                                                    object param = null,
                                                    IDbTransaction transaction = null,
                                                    IDbConnection connection = null)
        {
            try
            {
                List<T> result = new List<T>();
                var cd = new CommandDefinition(storeProcedureName, commandType: CommandType.StoredProcedure, parameters: param, transaction: transaction);

                var con = (transaction != null ? transaction.Connection : connection);

                if (con != null)
                {
                    var query = await con.QueryAsync<T>(cd);
                    result = query.ToList();
                    return result;
                }

                using (var cnn = GetConnection(appCode))
                {
                    var query = await cnn.QueryAsync<T>(cd);
                    result = query.ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Ghi log
                throw;
            }
        }
    }
}

