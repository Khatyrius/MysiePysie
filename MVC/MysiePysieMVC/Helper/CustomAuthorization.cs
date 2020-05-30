using System.Web;
using System.Web.Mvc;

namespace MysiePysieMVC.Helper
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        private readonly int[] allowedUserTypes;

        public CustomAuthorization(params int[] _types)
        {
            allowedUserTypes = _types;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }
    }
}