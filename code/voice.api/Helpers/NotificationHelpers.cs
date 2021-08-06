using voice.api.Controllers;
using voice.models;
using voice.notify;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace voice.api.Helpers
{
    public class NotificationHelpers : IDisposable
    {
        #region Object Declarations And Customer

        private IStringLocalizer<BaseController> Localizer { get; set; }

        private IMessages Messages { get; set; }

        private IHubContext<SignalRHelpers> HubContext { get; set; }

        public NotificationHelpers(
            IStringLocalizer<BaseController> Localizer,
            IMessages Messages,
            IHubContext<SignalRHelpers> HubContext = null)
        {
            this.Localizer = Localizer;
            this.Messages = Messages;
            this.HubContext = HubContext;
        }

        #endregion Object Declarations And Customer

        #region Send Customer Notifications

        public async Task SendCustomerNotifications(
            string role,
            string status,
            dynamic response,
            string updatedBy,
            bool isFreshEntry = false)
        {
            if (string.IsNullOrEmpty(role)) return;
            if (string.IsNullOrEmpty(status)) return;
            if (response == null) return;

            //var statusArray = new string[] { StatusConsts.Rejected, StatusConsts.Approved, StatusConsts.Reviewed, StatusConsts.Correction, StatusConsts.AutoApproved };

            //var message = new CustomerHelpers().GetRemarks(response.Name, status, updatedBy, role, isFreshEntry);

            //if (status.Equals(StatusConsts.Pending))
            //{
            //    var userIds = $"{response?.UserIdCsv},{response?.DmaUserIdCsv}";

            //    string statusByga = response?.StatusByGa;
            //    if (!string.IsNullOrEmpty(statusByga) && statusByga.Equals(StatusConsts.Correction))
            //        userIds = $"{userIds},{response?.GaUserIdCsv}";

            //    await new NotificationHelpers(Localizer, Messages, HubContext).SendWebNotifications(NotificationTypeConsts.CustomerFormCorrection, message, userIds);
            //}

            //if (status.Equals(StatusConsts.Correction))
            //    SendPushNotification(message, response.DeviceId, NotificationTypeConsts.CustomerFormCorrection);

            //if (statusArray.Contains(status))
            //{
            //    if (role.Equals(RoleConsts.Ga))
            //        await SendWebNotifications(NotificationTypeConsts.CustomerFormCorrection, message, response.DmaUserIdCsv);

            //    if (role.Equals(RoleConsts.Dma))
            //        await SendWebNotifications(NotificationTypeConsts.CustomerFormCorrection, message, response.GaUserIdCsv);
            //}
        }

        #endregion Send Customer Notifications

        #region Send Payment Push Notification to DMA Staff

        internal void SendPaymentNotification(
            dynamic response,
            string status)
        {
            if (response == null) return;
            if (string.IsNullOrEmpty(status)) return;

            var message = string.Empty;
            //if (status.Equals(StatusConsts.Successful)) message = "notification_payment_successful";
            //else if (status.Equals(StatusConsts.Failed)) message = "notification_payment_failed";

            SendPushNotification(message, response.DeviceId, NotificationTypeConsts.PaymentTransactionStatusResponse);
        }

        #endregion Send Payment Push Notification to DMA Staff

        #region Send SMS Notifications

        public async Task SendSmsNotificationAsync(
            string mobile,
            string data)
        {
            if (string.IsNullOrEmpty(mobile)) return;
            if (string.IsNullOrEmpty(data)) return;

            await Messages.SendSms(mobile, Localizer[data].Value);
        }

        #endregion Send SMS Notifications

        #region Send Push Notifications

        public void SendPushNotification(
            string data,
            string deviceToken,
            string notificationType)
        {
            if (string.IsNullOrEmpty(data)) return;
            if (string.IsNullOrEmpty(deviceToken)) return;
            if (string.IsNullOrEmpty(notificationType)) return;

            Messages.SendPush(Localizer[data].Value, deviceToken, notificationType, DeviceTypeEnums.android);
        }

        #endregion Send Push Notifications

        #region Send Web Notification

        public async Task SendWebNotifications(
            string type,
            string _message,
            string userIdCsv = null)
        {
            using var signalRHelp = new SignalRHelpers(HubContext);

            if (string.IsNullOrEmpty(userIdCsv))
            {
                await signalRHelp.SendMessage(type, new { message = _message });
                return;
            }

            var userIdList = userIdCsv.Split(",");
            foreach (var userId in userIdList)
            {
                await signalRHelp.SendMessage(type, new { message = _message, user = userId.Trim() });
            }
        }

        #endregion Send Web Notification

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}