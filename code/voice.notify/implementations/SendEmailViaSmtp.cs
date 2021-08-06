using System;
using System.Net;
using System.Net.Mail;
using voice.logs;
using voice.models;

namespace voice.notify.implementations
{
    public class SendEmailViaSmtp : IDisposable
    {
        #region Object Declaration And Constructor

        private EmailConfigs emailConfig;

        private AppConfigs appConfig;

        private readonly ILogManager Logger;

        public SendEmailViaSmtp(EmailConfigs emailConfigs, AppConfigs appConfig, ILogManager loguer)
        {
            this.appConfig = appConfig;
            this.emailConfig = emailConfigs;
            this.Logger = loguer;
        }

        #endregion Object Declaration And Constructor

        #region Send

        public void Send(string receiverEmail, string subject, string body, string EmailCC = null, string EmailBCC = null)
        {
            try
            {
                SendEmail(receiverEmail, subject, body, string.Empty, EmailCC, EmailBCC);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex.Message, ex);
            }
        }

        public void SendWithAttachment(string receiverEmail, string subject, string body, string sourceFilePath, string EmailCC = null, string EmailBCC = null)
        {
            try
            {
                SendEmail(receiverEmail, subject, body, sourceFilePath, EmailCC, EmailBCC);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex.Message, ex);
            }
        }

        private void SendEmail(string receiverEmail, string subject, string body, string sourceFilePath, string EmailCC, string EmailBCC)
        {
            //if (string.IsNullOrEmpty(receiverEmail)) throw new Exception($"Email can not be null.");
            //if (string.IsNullOrEmpty(emailConfig.SenderEmail)) throw new Exception($"Please specify the Sender Email in AppSettings.");
            //if (string.IsNullOrEmpty(emailConfig.SmtpAddress)) throw new Exception($"Please specify the SMTP Address in AppSettings.");
            //if (string.IsNullOrEmpty(emailConfig.Password)) throw new Exception($"Please specify the Password in AppSettings.");
            //if (string.IsNullOrEmpty(emailConfig.Port.ToString())) throw new Exception($"Please specify the Port in AppSettings.");
            ////if (string.IsNullOrEmpty(emailConfig.ServiceURL.ToString())) throw new Exception($"Please specify the ServiceURL in AppSettings.");
            //if (string.IsNullOrEmpty(emailConfig.UserName.ToString())) throw new Exception($"Please specify the UserName in AppSettings.");

            //ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010);
            ////service.Url = new Uri(emailConfig.ServiceURL);
            //service.Credentials = new NetworkCredential(emailConfig.UserName, emailConfig.Password);

            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //EmailMessage message = new EmailMessage(service);
            //message.From = emailConfig.SenderEmail;
            //message.Subject = new MessageBody(BodyType.HTML, subject);
            //message.Body = new MessageBody(BodyType.HTML, body);

            //if (!string.IsNullOrEmpty(sourceFilePath))
            //    message.Attachments.AddFileAttachment(sourceFilePath);

            //if (!string.IsNullOrWhiteSpace(receiverEmail))
            //{
            //    var multipleEmails = receiverEmail.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (var item in multipleEmails)
            //    {
            //        message.ToRecipients.Add(item);
            //    }
            //}

            //if (!string.IsNullOrEmpty(EmailCC))
            //{
            //    message.CcRecipients.Add(EmailCC);
            //}

            //if (!string.IsNullOrEmpty(EmailBCC))
            //{
            //    var ccList = EmailBCC.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (var item in ccList)
            //    {
            //        message.BccRecipients.Add(item);
            //    }
            //}
            //message.Send();



            if (string.IsNullOrEmpty(receiverEmail))
                throw new Exception($"Email can not be null.");
            if (string.IsNullOrEmpty(emailConfig.SenderEmail))
                throw new Exception($"Please specify the Sender Email in AppSettings.");
            if (string.IsNullOrEmpty(emailConfig.SmtpAddress))
                throw new Exception($"Please specify the SMTP Address in AppSettings.");
            if (string.IsNullOrEmpty(emailConfig.Password))
                throw new Exception($"Please specify the Password in AppSettings.");
            if (string.IsNullOrEmpty(emailConfig.Port.ToString()))
                throw new Exception($"Please specify the Port in AppSettings.");

            try
            {
                using var mail = new MailMessage
                {
                    From = new MailAddress(emailConfig.SenderEmail, emailConfig.DisplayName)
                };
                mail.To.Add(receiverEmail);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;
                using SmtpClient smtp = new SmtpClient(emailConfig.SmtpAddress, emailConfig.Port);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(emailConfig.SenderEmail, emailConfig.Password);
                smtp.Host = emailConfig.SmtpAddress;
                smtp.Port = emailConfig.Port;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(System.Text.Json.JsonSerializer.Serialize(new { class_name = this.GetType().Name, exception = ex }));
            }

        }

        #endregion Send

        #region Design Email

        public string DesignEmail(AppConfigs appConfig, string Subject, string Body)
        {
            return $"<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center' style='max-width:600px;padding-top:0px;font-family:Open Sans;font-weight:normal;font-size:14px;line-height:19px;color:#444444;border-collapse:collapse;background-color:#ffffff!important'> <tbody> <tr> <td> <table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#f5f5f5' style='background-color:#f5f5f5!important;font-family:Open Sans;font-weight:normal;font-size:14px;line-height:19px;color:#444444;border-collapse:collapse;border:1px solid #dddddd'> <tbody> <tr> <td width='4%'>&nbsp;</td><td height='76' width='92%'>{appConfig.Name} - {appConfig.TagLine}</td><td width='4%'>&nbsp;</td></tr></tbody> </table> <table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#ffffff' style='font-family:Open Sans;font-weight:normal;font-size:14px;line-height:19px;color:#444444;border-collapse:collapse;background-color:#ffffff!important;border-left:1px solid #dddddd;border-right:1px solid #dddddd'> <tbody> <tr> <td width='4%'></td><td width='92%' valign='top' style='padding:0 0 13px 0'> <table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-family:Open Sans;border-collapse:collapse;color:#444444'> <tbody> <tr> <td valign='top' style='font-size:14px;padding:20px 0 0 0'></td></tr><tr> <td valign='top' style='font-size:30px;padding:5px 0;line-height:35px'> <strong>{Subject}</strong> </td></tr></tbody> </table> <p style='line-height:20px!important;font-size:16px!important;margin-bottom:0!important;margin-bottom:0'> </p><div style='font-family:Open Sans;font-size:15px'>{Body}</div><p></p></td><td width='4%'></td></tr></tbody> </table> <table width='100%' cellspacing='0' cellpadding='0' border='0' style='font-family:Open Sans;font-weight:normal;font-size:14px;line-height:19px;color:#444444;border-collapse:collapse;border-left:1px solid #dddddd;border-right:1px solid #dddddd'> <tbody> <tr> <td width='4%'></td><td width='92%'> <p style='font-family:Open Sans;font-weight:normal;font-size:14px;line-height:19px;margin-top:0!important;margin-bottom:0!important;color:#444444'>Generic dotNET Core 3.1 Authority<br /><strong>{appConfig.Name}</strong> </p></td><td width='4%'></td></tr><tr> <td height='35' colspan='3'></td></tr></tbody> </table> <table width='100%' border='0' cellspacing='0' cellpadding='0' style='border:1px solid #dddddd;font-family:Open Sans;font-weight:normal;font-size:14px;line-height:19px;color:#444444;border-collapse:collapse;background-color:#f5f5f5!important' bgcolor='#f5f5f5'> <tbody> <tr> <td width='4%'></td><td width='92%' align='left' valign='middle' style='padding:13px'>{appConfig.Footer}</td><td width='4%'></td></tr></tbody> </table> </td></tr></tbody></table>";
        }

        #endregion Design Email

        #region House Keeping

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                emailConfig = null;
                appConfig = null;
            }
        }

        #endregion House Keeping
    }
}