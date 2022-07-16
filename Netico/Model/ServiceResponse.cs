using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// Dữ liệu trả về cho client
    /// </summary>
    public class ServiceResponse
    {
        public int Subcode { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; } = true;
    }
}
