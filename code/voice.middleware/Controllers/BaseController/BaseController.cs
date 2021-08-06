using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Text;

namespace voice.middleware.Controllers
{
    public class BaseController : Controller
    {
        #region View Response
        public ActionResult ViewResponse()
        {
            return View();
        }

        public ActionResult ViewResponse(string view_name)
        {
            return View(view_name);
        }
        #endregion View Response

        #region Error Response
        public ActionResult JsonErrorWithResponse(List<string> error)
        {
            return Json(new { type = "error", value = error });
        }

        public ActionResult JsonErrorResponse(string error)
        {
            return Json(new { type = "error", value = error });
        }

        public ActionResult JsonErrorResponseList(string[] error)
        {
            return Json(new { type = "error", value = error });
        }

        public ActionResult JsonErrorResponse(ModelStateDictionary error)
        {
            StringBuilder Errors = new StringBuilder();
            foreach (var e in error)
            {
                if (e.Value.Errors.Count > 0)
                    Errors.Append(e.Value.Errors[0].ErrorMessage + "<br />");
            }
            return Json(new { type = "error", value = Errors.ToString() });
        }
        #endregion Error Response

        #region Success Response
        public ActionResult JsonSuccessResponse()
        {
            return Json(new { type = "success", value = "Operation successful completed." });
        }

        public ActionResult JsonSuccessResponse(string success)
        {
            return Json(new { type = "success", value = success });
        }

        public ActionResult JsonSuccessResponse(object success)
        {
            return Json(new { type = "success", value = success });
        }

        public ActionResult JsonSuccessResponse(string success, string returnUrl)
        {
            return Json(new { type = "success", value = success, returnUrl = returnUrl });
        }

        public ActionResult JsonRedirectResponse(string returnUrl)
        {
            return Json(new { returnUrl = returnUrl });
        }
        #endregion Success Response

        #region Token Expried
        public ActionResult TokenManagement(HttpContext context)
        {
            context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        public ActionResult SessionExpired()
        {
            return RedirectToAction("HandleError", "Auth", new { sessionExpired = true });
        }
        #endregion Token Expried
    }
}