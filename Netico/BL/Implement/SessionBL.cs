using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BL
{
    public class SessionBL : ISessionBL
    {
        protected readonly IDatabaseServiceBL _databaseService;

        public SessionBL(IDatabaseServiceBL databaseService)
        {
            _databaseService = databaseService;
        }
        public async Task<Session> GetAssessTokenByID(Guid sessionID)
        {
            var sql = @"SELECT * FROM session s WHERE s.SessionID = @SessionID";
            var param = new Dictionary<string, object>
            {
                { "SessionID", sessionID }
            };

            var data = await _databaseService.QueryCommandText<Session>("niteko", sql, param);

            if (data is null) return new Session();

            return data[0];
        }
    }
}
