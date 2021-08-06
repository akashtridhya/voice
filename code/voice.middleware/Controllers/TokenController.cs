using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using voice.middleware.Helpers;
using voice.middleware.Models.Consts;
using voice.middleware.Models.Token.Request;
using voice.middleware.Models.Token.Response;

namespace voice.middleware.Controllers
{
    [Authorize]
    public class TokenController : BaseController
    {
        [Route("token-list", Name = "TokenList")]

        public async Task<IActionResult> TokenList()
        {
            var tokenList = await BonusTokenHelpers.TokenList();
            if (tokenList.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        public async Task<List<Detail>> GetToken()
        {
            var usersList = await BonusTokenHelpers.TokenList();
            var response = JsonConvert.DeserializeObject<TokenListResponse>(usersList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }

        [HttpPost]
        public async Task<ActionResult> TokenStatusOrDelete([FromBody] TokenStatusRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            var response = await BonusTokenHelpers.StatusToken(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> TokenPersist([FromBody] PersistsTokenRequest request)
        {
            if (request.id == "")
                request.id = null;
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await BonusTokenHelpers.PersistToken(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
    }
}