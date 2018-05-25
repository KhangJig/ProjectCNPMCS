using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace NYCshop.Attributes
{
    public class CheckAnynomousAttribute : FilterAttribute, IAuthenticationFilter
    {
        private string requiredURL = string.Empty;

        public void OnAuthentication(AuthenticationContext context)
        {
            if (context.HttpContext.Session["Username"] != null && context.HttpContext.Session["Role"] != null)
            {

            }
            else
            {
                context.Result = new HttpUnauthorizedResult(); // mark unauthorized
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext context)
        {
            string requestUrl = context.HttpContext.Request.RawUrl;

            // nếu người dùng chưa đăng nhập thì chuyển đến trang Account/Login
            if (context.HttpContext.Session["Username"] == null)
            {
                // nếu đã đăng xuất thì chuyển về trang chủ
                if (context.HttpContext.Request.RawUrl.Contains("LogOff"))
                    context.Result = new RedirectToRouteResult("Default",
                    new System.Web.Routing.RouteValueDictionary{
                        {"controller", "Home"},
                        {"action", "Index"}
                    });
                else
                    context.Result = new RedirectToRouteResult("Default",
                        new System.Web.Routing.RouteValueDictionary{
                        {"controller", "Account"},
                        {"action", "Login"},
                        {"returnUrl", requestUrl}
                        });
            }
        }
    }
}