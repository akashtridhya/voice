using voice.logs;
using voice.models;
using voice.queue;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace voice.notify.implementations
{
    public class Messages : IMessages
    {
        #region Object Declarations And Constructor

        private EmailConfigs EmailConfig { get; set; }

        private SmsConfigs SmsConfigs { get; set; }

        private AppConfigs AppConfig { get; set; }

        private ILogManager Loguer { get; set; }

        private PushConfigs PushConfig { get; set; }

        private IHostingEnvironment HostingEnvironment { get; set; }

        private IQueue Queue { get; set; }

        public Messages(
            IOptions<EmailConfigs> EmailConfigOption,
            IOptions<PushConfigs> PushConfigOptions,
            ILogManager loguer,
            IHostingEnvironment HostingEnvironment,
            IOptions<AppConfigs> AppConfigOption,
            IOptions<SmsConfigs> SmsConfigOption,
            IQueue Queue)
        {
            this.EmailConfig = EmailConfigOption.Value;
            this.AppConfig = AppConfigOption.Value;
            this.PushConfig = PushConfigOptions.Value;
            this.HostingEnvironment = HostingEnvironment;
            this.Loguer = loguer;
            this.Queue = Queue;
            this.SmsConfigs = SmsConfigOption.Value;
        }

        #endregion Object Declarations And Constructor

        /// <summary>
        /// Send an email.
        /// </summary>
        /// <param name="email">Received email id</param>
        /// <param name="subject">Mail Subject</param>
        /// <param name="body">Mail HTML body</param>
        public void SendEmail(string email, string subject, string body)
        {
            Queue.EnqueueAsync(() =>
            {
                using SendEmailViaSmtp smtp = new SendEmailViaSmtp(EmailConfig, AppConfig, Loguer);
                smtp.Send(email, subject, body);
            });
        }

        /// <summary>
        /// Send an email with Attachment
        /// </summary>
        /// <param name="email">Received email id</param>
        /// <param name="subject">Mail Subject</param>
        /// <param name="body">Mail HTML body</param>
        /// <param name="sourceFilePath">source file path</param>
        public void SendEmailWithAttachment(string email, string subject, string body, string sourceFilePath)
        {
            Queue.EnqueueAsync(() =>
            {
                using SendEmailViaSmtp smtp = new SendEmailViaSmtp(
                    EmailConfig,
                    AppConfig,
                    Loguer);
                smtp.SendWithAttachment(
                    email,
                    subject,
                    body,
                    sourceFilePath);
            });
        }

        /// <summary>
        /// Send Push notification to all the devices. [Currently Android and iPhone]
        /// </summary>
        /// <param name="data">Data in Json format</param>
        /// <param name="Token">Device token generate by Mobile Application</param>
        /// <param name="Device">Device Type Like, Android, iPhone</param>
        public void SendPush(string data, string token, string notificationType, DeviceTypeEnums device)
        {
            Queue.EnqueueAsync(() =>
            {
                switch (device)
                {
                    case (DeviceTypeEnums.android):
                        Android_FCM android = new Android_FCM(Loguer, PushConfig);
                        android.Send(data, token, notificationType);
                        break;

                    case (DeviceTypeEnums.ios):
                        Apple_APNS apple = new Apple_APNS(HostingEnvironment, Loguer, PushConfig);
                        apple.Send(data, token);
                        break;
                }
            });
        }

        /// <summary>
        /// Send SMS Notification
        /// </summary>
        /// <param name="mobile">Receiver's Mobile Number</param>
        /// <param name="body">Message Content</param>
        /// <returns></returns>
        public async Task SendSms(string mobile, string body)
        {
            await Queue.EnqueueAsync(async () =>
             {
                 try
                 {
                     HttpClient client = new HttpClient();
                     var url = string.Format(SmsConfigs.BaseUrl, SmsConfigs.SmsUserName, SmsConfigs.SmsPassword, SmsConfigs.ApiId, mobile, body, SmsConfigs.EntityId, SmsConfigs.TempId);
                     HttpResponseMessage response = await client.GetAsync(url);

                     if (response != null && !response.IsSuccessStatusCode)
                     {
                         Loguer.LogError(System.Text.Json.JsonSerializer.Serialize(new { class_name = this.GetType().Name, exception = response }));
                     }
                 }
                 catch (Exception ex)
                 {
                     this.Loguer.LogError("SMS", ex);
                 }
             });
        }

        #region Dispose

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion Dispose
    }
}