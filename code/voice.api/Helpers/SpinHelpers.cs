using System;
using System.Threading.Tasks;
using voice.dapper;
using voice.files;
using voice.models;
using voice.models.Requests.Spin;
using voice.models.Requests.UserManagement;

namespace voice.api.Helpers
{
    public class SpinHelpers : IDisposable
    {
        #region Object Declarations and Constructor
        private IDapperRepository DapperRepository { get; set; }

        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        private IUploadManager UploadManager { get; set; }

        public SpinHelpers(
            IDapperRepository DapperRepository = null,
            BaseUrlConfigs BaseUrlConfigs = null,
            IUploadManager UploadManager = null)
        {
            this.DapperRepository = DapperRepository;
            this.BaseUrlConfigs = BaseUrlConfigs;
            this.UploadManager = UploadManager;
        }
        #endregion Object Declarations and Constructor

        #region Persist Spin
        public async Task SpinPersist(SpinPersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.SpinManagement.SpinPersist, request);
        }
        #endregion Persist Spin

        #region Spin Status UpdateOrDelete
        public async Task SpinStatusUpdateOrDelete(DeleteRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.SpinManagement.SpinStatusOrDelete, request);
        }
        #endregion Spin Status UpdateOrDelete

        #region Dispose
        public void Dispose() => GC.SuppressFinalize(this);
        #endregion Dispose
    }
}
