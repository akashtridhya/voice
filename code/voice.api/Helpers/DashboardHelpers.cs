using System;
using voice.dapper;
using voice.security;

namespace voice.api.Helpers
{
    public class DashboardHelpers : IDisposable
    {
        #region Object Declarations And Constructor

        private ICrypto Crypto { get; set; }

        private IDapperRepository DapperRepository { get; set; }
        public DashboardHelpers(
            IDapperRepository DapperRepository,
            ICrypto Crypto)
        {
            this.DapperRepository = DapperRepository;
            this.Crypto = Crypto;
        }
        #endregion Object Declarations And Constructor

        #region Get DashBoard Data
        
        #endregion Get DashBoard Data

        #region Dispose
        public void Dispose() => GC.SuppressFinalize(this);
        #endregion Dispose
    }
}