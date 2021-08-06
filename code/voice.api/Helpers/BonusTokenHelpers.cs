using System;
using System.Threading.Tasks;
using voice.dapper;
using voice.models;
using voice.models.Requests.Token.Request;
using voice.models.Requests.UserManagement;

namespace voice.api.Helpers
{
    public class BonusTokenHelpers : IDisposable
    {
        #region Object Declarations And Constructor
        private IDapperRepository DapperRepository { get; set; }
        public BonusTokenHelpers(
            IDapperRepository DapperRepository)
        {
            this.DapperRepository = DapperRepository;
        }
        #endregion Object Declarations And Constructor

        #region Persist Token
        public async Task TokenPersist(TokenPersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.TokenManagement.TokenPersist, request);
        }
        #endregion Persist Token

        #region Token Status UpdateOrDelete
        public async Task TokenStatus(DeleteRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.TokenManagement.TokenStatus, request);
        }
        #endregion Token Status UpdateOrDelete

        #region Dispose
        public void Dispose() => GC.SuppressFinalize(this);
        #endregion Dispose
    }
}