using System;
using System.Threading.Tasks;
using voice.dapper;
using voice.files;
using voice.models;
using voice.models.Requests.Level;
using voice.models.Requests.UserManagement;

namespace voice.api.Helpers
{
    public class LevelHelpers : IDisposable
    {
        #region 1. Object Declarations and Constructor
        private IDapperRepository DapperRepository { get; set; }

        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        private IUploadManager UploadManager { get; set; }

        public LevelHelpers(
            IDapperRepository DapperRepository = null,
            BaseUrlConfigs BaseUrlConfigs = null,
            IUploadManager UploadManager = null)
        {
            this.DapperRepository = DapperRepository;
            this.BaseUrlConfigs = BaseUrlConfigs;
            this.UploadManager = UploadManager;
        }
        #endregion 1. Object Declarations and Constructor
       
        #region 2. Persist Level
        public async Task LevelPersist(LevelPersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.LevelManagement.LevelPersist, request);
        }
        #endregion 2. Persist Level

        #region 3. Level Status UpdateOrDelete
        public async Task LevelStatusUpdateOrDelete(DeleteRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.LevelManagement.LevelStatus, request);
        }
        #endregion 3. Level Status UpdateOrDelete
            
        #region Dispose
        public void Dispose() => GC.SuppressFinalize(this);
        #endregion Dispose
    }
}