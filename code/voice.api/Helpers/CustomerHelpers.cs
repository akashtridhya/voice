using System;
using voice.dapper;
using voice.files;
using voice.models;

namespace voice.api.Helpers
{
    public class CustomerHelpers : IDisposable
    {
        #region Object Declarations and Constructor

        private IDapperRepository DapperRepository { get; set; }

        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        private IUploadManager UploadManager { get; set; }

        public CustomerHelpers(
            IDapperRepository DapperRepository = null,
            BaseUrlConfigs BaseUrlConfigs = null,
            IUploadManager UploadManager = null)
        {
            this.DapperRepository = DapperRepository;
            this.BaseUrlConfigs = BaseUrlConfigs;
            this.UploadManager = UploadManager;
        }

        #endregion Object Declarations and Constructor

        #region Get Remarks of Customer Registration Form

        internal string GetRemarks(
            string customerName,
            string status,
            string username,
            string role,
            bool isFreshEntry = false)
        {
            var displayRole = GetRole(role);
            var notificationDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");

            if (status.ToLower().Equals("pending") && !isFreshEntry)
            {
                return $"{customerName}'s corrected registration form has been submitted by {username} ({displayRole}) | {notificationDate}";
            }

            return (status.Trim().ToLower()) switch
            {
                "pending" => $"{customerName}'s registration form has been submitted by {username} ({displayRole}) | {notificationDate}",
                "correction" => $"{customerName}'s registration form marked as Corrected by {username} ({displayRole}) | {notificationDate}",
                "approved" => $"{customerName}'s registration form marked as Approved by {username} ({displayRole}) | {notificationDate}",
                "rejected" => $"{customerName}'s registration form marked as Rejected by {username} ({displayRole}) | {notificationDate}",
                "reviewed" => $"{customerName}'s registration form marked as Reviewed by {username} ({displayRole}) | {notificationDate}",
                "auto approved" => $"{customerName}'s registration form marked as Auto Approved by {username} ({displayRole}) | {notificationDate}",
                _ => string.Empty,
            };
        }

        #endregion Get Remarks of Customer Registration Form

        #region Get Role Name

        public string GetRole(string role)
        {
            return (role.Trim()) switch
            {
                "admin" => "Admin",
                "subadmin" => "Sub Admin",
                "ga" => "GA",
                "dma" => "DMA",
                "dmastaff" => "DMA Staff",
                _ => string.Empty,
            };
        }

        #endregion Get Role Name

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}