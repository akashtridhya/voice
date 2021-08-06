using System;
using System.Threading.Tasks;
using voice.dapper;
using voice.files;
using voice.models;
using voice.models.Requests.General;
using voice.models.Requests.UserManagement;
using voice.models.Requests.XpPoints;

namespace voice.api.Helpers
{
    public class XpPointsHelpers : IDisposable
    {
        #region 1. Object Declarations and Constructor
        private IDapperRepository DapperRepository { get; set; }

        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        private IUploadManager UploadManager { get; set; }

        public XpPointsHelpers(
            IDapperRepository DapperRepository = null,
            BaseUrlConfigs BaseUrlConfigs = null,
            IUploadManager UploadManager = null)
        {
            this.DapperRepository = DapperRepository;
            this.BaseUrlConfigs = BaseUrlConfigs;
            this.UploadManager = UploadManager;
        }
        #endregion 1. Object Declarations and Constructor
       
        #region 2. Persist XpPoints
        public async Task XpPointsPersist(XpPointsPersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.XpPointsManagement.XpPointsPersist, request);
        }
        #endregion 2. Persist XpPoints

        #region 3. XpPoints Status UpdateOrDelete
        public async Task XpPointsStatusUpdateOrDelete(DeleteRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.XpPointsManagement.XpPointsStatus, request);
        }
        #endregion 3. XpPoints Status UpdateOrDelete
            
        #region Dispose
        public void Dispose() => GC.SuppressFinalize(this);
        #endregion Dispose
    }
}