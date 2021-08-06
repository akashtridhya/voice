using System;
using System.Threading.Tasks;
using voice.dapper;
using voice.files;
using voice.models;
using voice.models.Requests.General;
using voice.models.Requests.UserManagement;

namespace voice.api.Helpers
{
    public class GeneralHelpers : IDisposable
    {
        #region Object Declarations and Constructor
        private IDapperRepository DapperRepository { get; set; }

        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        private IUploadManager UploadManager { get; set; }

        public GeneralHelpers(
            IDapperRepository DapperRepository = null,
            BaseUrlConfigs BaseUrlConfigs = null,
            IUploadManager UploadManager = null)
        {
            this.DapperRepository = DapperRepository;
            this.BaseUrlConfigs = BaseUrlConfigs;
            this.UploadManager = UploadManager;
        }
        #endregion Object Declarations and Constructor

        #region 1. Terms and Conditions

        #region 1.1 Persist Terms and Conditions
        public async Task TermsAndConditionsPersist(TermsAndConditionPersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GeneralManagement.TermsAndConditionsPersist, request);
        }
        #endregion 1.1 Persist Terms and Conditions

        #endregion 1. Terms and Conditions

        #region 2. Privacy Policy

        #region 2.1 Persist Privacy Policy
        public async Task PrivacyPolicyPersist(PrivacyPolicyPersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GeneralManagement.PrivacyPolicyPersist, request);
        }
        #endregion 2.1 Persist Privacy Polivy

        #endregion 2. Privacy Policy

        #region 3. How To Play

        #region 3.1 Persist How To Play
        public async Task HowToPlayPersist(HowToPlayPersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GeneralManagement.HowToPlayPersist, request);
        }
        #endregion 3.1 Persist How To Play

        #endregion 3. How To Play

        #region 4. Dropdowns

        #region 4.1 Country
        #region 4.1.1 Persist Country
        public async Task CountryPersist(CountryPersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GeneralManagement.CountryPersist, request);
        }
        #endregion 4.1.1 Persist Country

        #region 4.1.2 Country Status UpdateOrDelete
        public async Task CountryStatusUpdateOrDelete(DeleteRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GeneralManagement.CountryStatusOrDelete, request);
        }
        #endregion 4.1.2 Country Status UpdateOrDelete
        #endregion 4.1 Country

        #region 4.2 Language
        #region 4.2.1 Persist Language
        public async Task LanguagePersist(LanguagePersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GeneralManagement.LanguagePersist, request);
        }
        #endregion 4.2.1 Persist Language

        #region 4.2.2 Language Status UpdateOrDelete
        public async Task LanguageStatusUpdateOrDelete(DeleteRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GeneralManagement.LanguageStatusOrDelete, request);
        }
        #endregion 4.2.2 Language Status UpdateOrDelete
        #endregion 4.2 Language

        #region 4.3 Currency
        #region 4.3.1 Persist Currency
        public async Task CurrencyPersist(CurrencyPersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GeneralManagement.CurrencyPersist, request);
        }
        #endregion 4.3.1 Persist Currency

        #region 4.3.2 Currency Status UpdateOrDelete
        public async Task CurrencyStatusUpdateOrDelete(DeleteRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GeneralManagement.CurrencyStatusOrDelete, request);
        }
        #endregion 4.3.2 Currency Status UpdateOrDelete
        #endregion 4.3 Currency
        
        #endregion 4. Dropdowns

        #region Dispose
        public void Dispose() => GC.SuppressFinalize(this);
        #endregion Dispose
    }
}