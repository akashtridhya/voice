using voice.api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace voice.api.Filters
{
    public class IdentityFilters1
    {
        #region Object Declarations And Constructor

        private readonly RequestDelegate _next;

        public IdentityFilters1(RequestDelegate next) => _next = next;

        #endregion Object Declarations And Constructor

        public async Task InvokeAsync(HttpContext context)
        {
            var localizer = (IStringLocalizer<BaseController>)context.RequestServices.GetService(typeof(IStringLocalizer<BaseController>));
            if (!context.Request.Headers["x-request-identity"].ToString().Equals("gu"))
            {
                context.Response.StatusCode = 405;
                context.Response.ContentType = "application/json";
                string jsonString = JsonConvert.SerializeObject(new { message = localizer["error_unknown_source"].Value });
                await context.Response.WriteAsync(jsonString, Encoding.UTF8);
                
                return;
            }

            await _next(context);
        }
    }
}