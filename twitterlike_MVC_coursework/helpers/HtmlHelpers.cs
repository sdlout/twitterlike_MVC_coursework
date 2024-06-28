using Microsoft.AspNetCore.Mvc.Rendering;

namespace twitterlike_MVC_coursework.Helpers;

public static class HtmlHelpers
{
    public static string IsDisabled(this IHtmlHelper html, string controller, string action)
    {
        var routeData = html.ViewContext.RouteData;
        var routeController = routeData.Values["controller"].ToString();
        var routeAction = routeData.Values["action"].ToString();

        return controller == routeController && action == routeAction ? "disabled" : "";
    }
}