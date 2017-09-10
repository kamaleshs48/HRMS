using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace HRMS.Filters
{
    public class UrlCopyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //write your user right logic 
            //if user has right to do nothig otherwise redirect to error page.
            string PName = "";
            Controller controller = filterContext.Controller as Controller;
            //  var url = filterContext.HttpContext.Request.UrlReferrer;

            if (filterContext.HttpContext.Request.UrlReferrer != null)
            {
                PName = filterContext.HttpContext.Request.UrlReferrer.Segments[filterContext.HttpContext.Request.UrlReferrer.Segments.Length - 1];
            }
            // filterContext.HttpContext.Session["Action"] = NewActionName;
            if (PName == "" || filterContext.HttpContext.Session["USERID"] == null)
            {
                string message = "You have no right to view this page.";
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("area", "");
                redirectTargetDictionary.Add("action", "Login");
                redirectTargetDictionary.Add("controller", "Home");
                // redirectTargetDictionary.Add("customMessage", message);
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }

        }
        private string Log(RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0}", actionName);
            // Debug.WriteLine(message, "Action Filter Log");
            return message;
        }
    }
}