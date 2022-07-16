using System.Web.Mvc;

namespace BL
{
    public class NitekoAuthorizeAttribute :  AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //var user = filterContext.HttpContext.User;
        }
    }
}
