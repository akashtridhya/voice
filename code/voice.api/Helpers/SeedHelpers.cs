using voice.api.Controllers;
using voice.dapper;
using voice.models;
using voice.notify;
using voice.security;
using Microsoft .Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace voice.api.Helpers
{
    public class SeedHelpers : IDisposable
    {
        #region Object Declarations And Constructor

        private IDapperRepository DapperRepository { get; set; }

        private IStringLocalizer<BaseController> Localizer { get; set; }

        private AdminConfigs AdminConfig { get; set; }

        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        private ICrypto Crypto { get; set; }

        private IMessages Messages { get; set; }

        public SeedHelpers(
            IDapperRepository DapperRepository,
            IOptions<AdminConfigs> AdminConfigOptions,
            IStringLocalizer<BaseController> Localizer,
            IOptions<BaseUrlConfigs> BaseUrlConfigsOptions,
            IMessages Messages,
            ICrypto Crypto)
        {
            this.Localizer = Localizer;
            this.Crypto = Crypto;
            AdminConfig = AdminConfigOptions.Value;
            BaseUrlConfigs = BaseUrlConfigsOptions.Value;
            this.Messages = Messages;
            this.DapperRepository = DapperRepository;
        }

        #endregion Object Declarations And Constructor

        #region Seed Data

        public async Task Seed()
        {
            var user = DapperRepository.Find<dynamic>(
                SpConsts.Account.SetSeedData,
                new
                {
                    EmailId = AdminConfig.Email,
                    Username = AdminConfig.Username,
                    Password = Crypto.EncryptPassword(AdminConfig.Password),
                });

            if (user != null && !user.EmailConfirmed)
            {
                #region Sending email in queue

                string token = new TokenHelpers(DapperRepository, Crypto).GenerateAdminInviteToken(user.Id.ToString());
                var Link = string.Format(BaseUrlConfigs.AdminInvite, token);

                //await new EmailHelpers(Localizer, Messages, BaseUrlConfigs, DapperRepository)
                //    .SendAccountEmail(new ProfileResponse { Id = user.Id, FirstName = user.FirstName, Role = user.Role, LastName = user.LastName, Mobile = user.Mobile, Email = user.Email }, Link, EmailTypeConst.AdminInvite.ToString());

                #endregion Sending email in queue
            }
        }

        #endregion Seed Data

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}