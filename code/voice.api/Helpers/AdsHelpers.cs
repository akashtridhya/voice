using System;
using System.Threading.Tasks;
using voice.dapper;
using voice.models;
using voice.models.Requests.Ads.Request;
using voice.models.Requests.UserManagement;
using voice.security;

namespace voice.api.Helpers
{
    public class AdsHelpers : IDisposable
    {
        #region Object Declarations And Constructor
        private IDapperRepository DapperRepository { get; set; }
        public AdsHelpers(
            IDapperRepository DapperRepository)
        {
            this.DapperRepository = DapperRepository;
        }
        #endregion Object Declarations And Constructor

        #region Persist Ads
        public async Task AdsPersist(AdsPersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.AdsManagement.AdsPersist, request);
        }
        #endregion Persist Ads

        #region Ads Status UpdateOrDelete
        public async Task AdsStatusUpdateOrDelete(DeleteRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.AdsManagement.AdsStatusOrDelete, request);
        }
        #endregion Ads Status UpdateOrDelete

        #region Dispose
        public void Dispose() => GC.SuppressFinalize(this);
        #endregion Dispose
    }
}