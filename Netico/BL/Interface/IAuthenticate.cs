using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface IAuthenticate
    {
        Task<ServiceResponse> Login(Customer customer, HttpResponse httpResponse);
    }
}
