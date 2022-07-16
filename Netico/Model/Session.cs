using System;

namespace Model
{
    public class Session
    {
        public int ID { get; set; }

        public Guid SessionID { get; set; }

        public DateTime? ExpireTime { get; set; }

        public DateTime? LoginTime { get; set; }

        public string AccessToken { get; set; }
    }
}
