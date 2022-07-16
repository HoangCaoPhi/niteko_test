using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class LogService : ILogService
    {
        private static Logger _currentLogger;
        private static Logger currentLogger
        {
            get
            {
                if (_currentLogger == null)
                {
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NLog.config");
                    _currentLogger = NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
                }
                return _currentLogger;
            }
        }

        public void LogError(Exception ex, string message, Dictionary<string, object> properties = null)
        {
            var info = new LogEventInfo()
            {
                Level = LogLevel.Error,
                Message = message,
                Exception = ex
            };

            currentLogger.Log(info);
        }
    }
}
