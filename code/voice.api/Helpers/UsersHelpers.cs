using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voice.dapper;
using voice.models;
using voice.models.Requests.UserManagement;
using voice.models.Responses.UserManagement;
using voice.security;

namespace voice.api.Helpers
{
    public class UsersHelpers : IDisposable
    {
        #region Object Declarations And Constructor

        private ICrypto Crypto { get; set; }

        private IDapperRepository DapperRepository { get; set; }
        public UsersHelpers(
            IDapperRepository DapperRepository)
        {
            this.DapperRepository = DapperRepository ?? throw new System.ArgumentNullException(nameof(DapperRepository));
            this.Crypto = Crypto;
        }
        #endregion Object Declarations And Constructor

        public async Task ProfileStatusUpdateOrDelete(DeleteRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.UserManagement.UsersStatusOrDelete, request);
        }

        public async Task UserActiveTimeInsert(UserActiveTimeInsertRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.UserManagement.UserTimeInsert, request);
        }

        public async Task UserBalancePersist(UserBalancePersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.UserManagement.UserBalancePersist, request);
        }

        public async Task<IEnumerable<UserLeaderBoardResponse>> UserLeaderBoard(UserLeaderBoardRequest request)
        {
            IEnumerable<UserLeaderBoardResponse> response = await DapperRepository.GetTableAsync<UserLeaderBoardResponse>(SpConsts.UserManagement.UserLeaderBoard, request);
            return response;
        }

        public async Task UserXpTimePersist(UserXpTimePersistRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.UserManagement.UserXpTimePersist, request);
        }

        public async Task AdminInsert(AdminInsertRequest request)
        {
            await DapperRepository.AddOrUpdateAsync(SpConsts.UserManagement.AdminInsert, request);
        }

        #region Dispose
        public void Dispose() => GC.SuppressFinalize(this);
        #endregion Dispose
    }
}