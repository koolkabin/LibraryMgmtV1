using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Library_Management.Filters
{
    public class AdminAuthorizeFilter : IActionFilter
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AdminAuthorizeFilter(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_contextAccessor.HttpContext.Session.GetInt32("userId") == null)
            {
                context.Result = new RedirectToActionResult("Index", "Account", null);
            }
            
            if (_contextAccessor.HttpContext.Session.GetString("userType") != "Admin")
            {
                context.Result = new ViewResult
                {
                    ViewName = "/Views/Shared/ErrorPage.cshtml"
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Empty implementation (even if you don't need specific behavior here)
        }
    }
}
