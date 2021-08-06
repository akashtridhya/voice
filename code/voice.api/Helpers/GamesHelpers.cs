using System;
using System.Threading.Tasks;
using voice.dapper;
using voice.models;
using voice.models.Requests.Game;
using voice.models.Requests.UserManagement;

namespace voice.api.Helpers
{
    public class GamesHelpers : IDisposable
    {
        #region Object Declarations And Constructor
        private IDapperRepository DapperRepository { get; set; }
        public GamesHelpers(
            IDapperRepository DapperRepository)
        {
            this.DapperRepository = DapperRepository;
        }
        #endregion Object Declarations And Constructor

        #region Game Persist
        public async Task GamePersist(PersistGameRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GameManagement.GamePersist, request);
        }
        #endregion Game Persist

        #region  Game Status UpdateOrDelete
        public async Task GameStatus(DeleteRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.GameManagement.GameStatus, request);
        }
        #endregion Game Status UpdateOrDelete

        #region House Keeping
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool dispose)
        {
            if (dispose)
            {
            }
        }
        #endregion
    }
}