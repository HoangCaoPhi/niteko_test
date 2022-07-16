using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface ISessionBL
    {
        Task<Session> GetAssessTokenByID(Guid sessionID);
    }
}
