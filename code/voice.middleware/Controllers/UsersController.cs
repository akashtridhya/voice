using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using voice.middleware.Helpers;
using voice.middleware.Models.Consts;
using voice.middleware.Models.Users.Request;
using voice.middleware.Models.Users.Response;

namespace voice.middleware.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {

        [Route("users-list", Name = "UsersList")]
        public async Task<IActionResult> UsersList()
        {
            var usersList = await UsersHelpers.UserList();
            if (usersList.meta.statusCode == StatusCodeConsts.UnAuthorized)
                return SessionExpired();
            return View();
        }

        public async Task<List<Detail>> GetUsers()
        {
            var usersList = await UsersHelpers.UserList();
            var response = JsonConvert.DeserializeObject<UsersListResponse>(usersList.data);
            for (int i = 0; response.details.Count > i; i++)
            {
                response.details[i].number = i + 1;
            }
            return response.details;
        }

        [HttpPost]
        public async Task<ActionResult> UserStatusOrDelete([FromBody] UserStatusRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);

            var response = await UsersHelpers.StatusUser(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }

        [HttpPost]
        public async Task<ActionResult> UserEdit([FromBody]UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return JsonErrorResponse(ModelState);
            var response = await UsersHelpers.UpdateUser(request);
            if (response.meta.statusCode != StatusCodeConsts.Success)
                return JsonErrorResponseList(response.meta.message);
            else
                return JsonSuccessResponse(response.meta.message);
        }
    }
}