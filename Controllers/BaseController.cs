// Controllers/BaseController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RealEstateAuction.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Kiểm tra session
            var userId = HttpContext.Session.GetString("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            // Nếu chưa đăng nhập
            if (string.IsNullOrEmpty(userId))
            {
                filterContext.Result = RedirectToAction("Login", "Account");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }

    // Controller riêng cho Admin, chỉ cho phép Role = "1"
    public class AdminBaseController : BaseController
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Kiểm tra role Admin
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "1")
            {
                // Không phải admin thì chuyển về trang chủ
                filterContext.Result = RedirectToAction("Index", "Home");
                return;
            }
        }
    }
}