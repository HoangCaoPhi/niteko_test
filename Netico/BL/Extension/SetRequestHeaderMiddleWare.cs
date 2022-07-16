using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BL
{
    public static class SetRequestHeaderMiddleWareExtension
    {
        public static IApplicationBuilder UseSetRequestHeaderMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SetRequestHeaderMiddleWare>();
        }
    }

    public class SetRequestHeaderMiddleWare
    {
        private readonly RequestDelegate _next;

        public SetRequestHeaderMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                if (context.Request.Path.ToString() == "/")
                {
                    await _next(context);
                }
                else
                {
                    var sessionID = "";
                    if (context.Request.Cookies.ContainsKey("x_sessionID"))
                    {
                        sessionID = context.Request.Cookies["x_sessionID"];
                    }
                    else
                    {
                        sessionID = context.Request.Headers["x_sessionID"];
                    }

                    if (sessionID is null)
                    {
                        context.Request.Headers["Authorization"] = "";
                        await _next(context);
                    }
                    else
                    {
                        var sessionBL = context.RequestServices.GetRequiredService<ISessionBL>();
                        var session = await sessionBL.GetAssessTokenByID(new Guid(sessionID));
                        context.Request.Headers["Authorization"] = $"Bearer { session.AccessToken}";
                        await _next(context);
                    }
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}

