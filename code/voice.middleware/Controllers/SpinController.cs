using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using voice.middleware.Helpers;
using voice.middleware.Models.Consts;
using voice.middleware.Models.General.Country.Request;
using voice.middleware.Models.Spin.Request;
using voice.middleware.Models.Spin.Response;

namespace voice.middleware.Controllers
{
    public class SpinController : BaseController
    {
        [Route("spin", Name = "Spin")]

        public async Task<IActionResult> SpinList()
        {
            var SpinList = await SpinHelpers.SpinList();
            if (SpinList.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        public async Task<List<DetailSpin>> GetSpin()
        {
            var usersList = await SpinHelpers.SpinList();
            var response = JsonConvert.DeserializeObject<SpinListResponse>(usersList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }

        [HttpPost]
        public async Task<ActionResult> SpinStatusOrDelete([FromBody] StatusGeneralRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            var response = await SpinHelpers.StatusSpin(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> SpinPersist([FromBody] PersistSpinRequest request)
        {
            if (request.id == "")
                request.id = null;
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await SpinHelpers.PersistSpin(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
    }
}
