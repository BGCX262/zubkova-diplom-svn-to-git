using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DogsClub.WebUI.Helpers
{
    public static class Navigation
    {
        public static MvcHtmlString NavigationLink(this HtmlHelper html, string linkText, string actionName, string controllerName)
        {
            string currentController = string.IsNullOrEmpty((html.ViewContext.RouteData.Values["controller"] as string)) ? "Home" :
                    html.ViewContext.RouteData.Values["controller"] as string;

            string currentAction = string.IsNullOrEmpty((html.ViewContext.RouteData.Values["action"] as string)) ? "Index" :
                    html.ViewContext.RouteData.Values["action"] as string;

            string currentLinkClass = string.Empty;
            if (string.Equals(actionName, currentAction, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase))
            {
                currentLinkClass = "active-menu";
            }
            System.Web.Mvc.UrlHelper urlHelper = new System.Web.Mvc.UrlHelper(html.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format("<a href=\"{0}\" class=\"{1}\">{2}</a>", urlHelper.Action(actionName, controllerName), currentLinkClass, linkText));
        }
    }
}