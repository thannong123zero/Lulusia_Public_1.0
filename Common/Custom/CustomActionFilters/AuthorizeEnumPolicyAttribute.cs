//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace Common.CustomActionFilters
//{
//    public class AuthorizeEnumPolicyAttribute : ActionFilterAttribute
//    {
//        private readonly string _policy;
//        public AuthorizeEnumPolicyAttribute(EController controllerName, EAction actionName)
//        {
//            _policy = controllerName + "_" + actionName;
//        }
//        public AuthorizeEnumPolicyAttribute(ERole role)
//        {
//            _policy = role.ToString();
//        }
//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            if (context.HttpContext.User == null || !context.HttpContext.User.Identity.IsAuthenticated)
//            {
//                // User is not authenticated, handle accordingly (e.g., redirect to login page)
//                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
//                {
//                    controller = "Login",
//                    action = "index"
//                }));
//            }
//            else
//            {
//                bool checkPolicy = context.HttpContext.User.Claims.Any(s => s.Type == _policy && s.Value == _policy);
//                // User is authenticated, check for specific permissions
//                if (context.HttpContext.User.IsInRole(ERole.Administrator.ToString()) || checkPolicy)
//                {
//                    base.OnActionExecuting(context);
//                }
//                else
//                {
//                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
//                    {
//                        controller = "Login",
//                        action = "AccessDenied"
//                    }));
//                }
//            }

//            base.OnActionExecuting(context);
//        }
//    }
//}
