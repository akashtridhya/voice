using voice.dapper;
using voice.files;
using voice.models;
using voice.security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace voice.api.Controllers
{
    public class GenericController : BaseController
    {
        #region 1. Object Declarations And Constructor
        public GenericController(
          IStringLocalizer<BaseController> Localizer,
          ICrypto Crypto,
          IDapperRepository DapperRepository)
        : base(DapperRepository, Localizer, Crypto) { }

        #endregion 1. Object Declarations And Constructor

        #region 2. Upload Attachments
        [Authorize]
        [HttpPost(ActionsConsts.Generic.AttachmentUoload)]
        public async Task<IActionResult> UploadCustomerImageAsync(
         AttachmentUploadRequest request,
         [FromServices] IUploadManager UploadManager)
        {
            if (!ModelState.IsValid) return ErrorResponse(ModelState);

            await ValidateUserAsync();

            if (request.Type.Equals(FolderEnums.error.ToString())) return ErrorResponse("bad_response_enter_valid_upload_type");

            if (!request.Type.Equals(FolderEnums.games.ToString())
                && !request.Type.Equals(FolderEnums.adImage.ToString()))
            {
                return ErrorResponse("bad_response_enter_valid_upload_type");
            }

            var response = await UploadManager.Store(
                request.File,
                Guid.NewGuid().ToString(),
                request.Type);

            return OkResponse(response);
        }
        #endregion 2. Upload Attachments

        #region 3. URL Shortener
        /// <summary>
        /// 1. Enter a valid random string.
        /// 2. Fetch source URL form Db based on random string.
        /// 3. Return source.
        /// </summary>
        /// <param name="shortURL"></param>
        /// <returns></returns>
        [HttpGet(ActionsConsts.Generic.GetSourceURL + "{ShortURL}")]
        public async Task<IActionResult> GetSourceURLAsynca([FromRoute] GetSourceURLRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ShortURL)) return ErrorResponse("Please provice Url");

            var sourceURL = await DapperRepository.FindAsync<SetShortURLRequest>(
                SpConsts.Generic.SelectRandomUrl,
                new
                {
                    request.ShortURL
                });

            if (sourceURL == null) return Ok(string.Empty);

            return Ok(sourceURL.SourceURL);
        }

        #endregion 3. URL Shortener
    }
}