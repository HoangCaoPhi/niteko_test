using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interface
{
    public interface IConfigService
    {
        /// <summary>
        /// Lấy config ở appsetting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetAppSetting(string key);

        /// <summary>
        /// Lấy key của jwt
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetJWT(string key);

        /// <summary>
        /// Lấy conenction string
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetConnectionString(string key);
    }
}
