using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface ILogService
    {
        void LogError(Exception ex, string message, Dictionary<string, object> properties = null);
    }
}
