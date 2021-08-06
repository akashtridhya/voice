using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using voice.api.Controllers;
using voice.dapper;
using voice.models;
using voice.notify;

namespace voice.api.Helpers
{
    public class EmailHelpers : IDisposable
    {
        #region Object Declarations And Constructor

        private IStringLocalizer<BaseController> Localizer { get; set; }

        private IMessages Messages { get; set; }

        private BaseUrlConfigs BaseUrlConfigs { get; set; }

        IDapperRepository DapperRepository { get; set; }

        public EmailHelpers(
            IStringLocalizer<BaseController> Localizer,
            IMessages Messages,
            BaseUrlConfigs BaseUrlConfigs,
            IDapperRepository DapperRepository)
        {
            this.Localizer = Localizer;
            this.Messages = Messages;
            this.BaseUrlConfigs = BaseUrlConfigs;
            this.DapperRepository = DapperRepository;
        }

        #endregion Object Declarations And Constructor

        #region Send Account related mail

        public async Task SendAccountEmail(
            ProfileResponse user,
            string url,
            string emailType)
        {
            //url = await new GenericHelpers(BaseUrlConfigs, DapperRepository).SetShortURLAsync(url);
            switch (emailType)
            {
                case EmailTypeConst.AdminInvite:
                    SendEmail(user.Email, $"{Localizer["email_admin_invite"].Value}", DesignEmail($"{Localizer["email_admin_invite"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {user.Username},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">You are invited to manage the {Localizer["app_name"].Value}. Click login button to manage {Localizer["app_name"].Value}</td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Link:</td></tr><tr><td style="" line-height: 0px;"" height=""4""></td></tr><tr><td><a href=""{url}"" style=""color: #005093; font-family: 'Nunito', sans-serif;"">{url}</a></td></tr>"));
                    break;

                case EmailTypeConst.ResetPassword:
                    SendEmail(user.Email, $"{Localizer["email_sub_reset_password"].Value}", DesignEmail($"{Localizer["email_sub_reset_password"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {user.Username},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">We have recieved request to reset your The Voice account password, Please use the link below to reset your password.</td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Link :</td></tr><tr><td style="" line-height: 0px;"" height=""4""></td></tr><tr><td><a href=""{url}"" style=""color: #005093; font-family: 'Nunito', sans-serif;"">{url}</a></td></tr>"));
                    break;

                case EmailTypeConst.RedirectToLogin:
                    SendEmail(user.Email, $"{Localizer["email_user_register"].Value}", DesignEmail($"{Localizer["email_user_register"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {user.Username}, </td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Your account is registerd in {Localizer["app_name"].Value}. <br/> Here are your credentials: <br/> Email : <b>{user.Email}</b> <br/> Password: <b></b> <br/></td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr align=""center""><td><a href=""{url}""style=""font-family: 'Nunito', sans-serif; padding: 12px; min-width: 216px; display: inline-block; background: #4cb034; border-radius: 4px; font-size: 14px; line-height: 20px; color: #fff;"">Login</a></td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">If that doesn’t work, copy and paste the following link in your browser:</td></tr><tr><td style="" line-height: 0px;"" height=""4""></td></tr><tr><td><a href=""{url}"" style=""color: #005093; font-family: 'Nunito', sans-serif;"">{url}</a></td></tr>"));
                    break;

                case EmailTypeConst.ConfirmEmail:
                    SendEmail(user.Email, $"{Localizer["email_sub_confirm_email"].Value} | {Localizer["app_name"].Value}", $"Hi {user.Username}, <br /><br />Thanks a lot for registering with {Localizer["app_name"].Value}, Please <a href='{url}'>click here</a> to confirm your email.<br /><br />Thank You.");
                    break;

                case EmailTypeConst.ChangeEmail:
                    SendEmail(user.Email, $"{Localizer["email_sub_change_email"].Value} | {Localizer["app_name"].Value}", $"Hi {user.Username},<br /><br />Thank you for updating your email. Please confirm the email by clicking this link:<br /><a href='{url}'>click here</a> to confirm your new email.<br /><br />Thank You.");
                    break;

                case EmailTypeConst.ChangePassword:
                    SendEmail(user.Email, $"{Localizer["email_sub_change_password"].Value}", DesignEmail($"{Localizer["email_sub_change_password"].Value}", @$"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {user.Username},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Your password for the The Voice account hasbeen updated recently. Ignore this email if you have made this changes, contact The Voice if you have not made this changes.</td></tr>"));
                    break;

                case EmailTypeConst.SendPassword:
                    SendEmail(user.Email, $"{Localizer["email_sub_change_password"].Value}", DesignEmail($"{Localizer["email_sub_change_password"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {user.Username},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Your password for the {Localizer["app_name"].Value} account has been updated recently by your Admin. <br/> Your new password is : <b></b> </td></tr>"));
                    break;
                case EmailTypeConst.RegisterUser:
                    SendEmail(user.Email, $"{Localizer["email_user_register"].Value}", DesignEmail($"{Localizer["email_user_register"].Value}", $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {user.Username}, </td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Your account is registerd in {Localizer["app_name"].Value}. <br/> Here are your details: <br/> Username : <b>{user.Username}</b><br/> Email : <b>{user.Email}</b></td></tr>"));
                    break;
            }
        }

        #endregion Send Account related mail

        #region Send Payment Related Emails  (i.e. Initiate, Success, Failed)

        public async Task SendPaymentTransationEmail(
            string email,
            string name,
            string url)
        {
            //url = await new GenericHelpers(BaseUrlConfigs, DapperRepository).SetShortURLAsync(url);
            //var mailBody = $@"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {name},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Your The Voice Registration Form Submitted, Please use the link below to pay your registration amount.</td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr align=""center""><td><a href=""{url}""style=""font-family: 'Nunito', sans-serif; padding: 12px; min-width: 216px; display: inline-block; background: #4cb034; border-radius: 4px; font-size: 14px; line-height: 20px; color: #fff;"">Make Payment</a></td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">If that doesn’t work, copy and paste thefollowing link in your browser:</td></tr><tr><td style="" line-height: 0px;"" height=""4""></td></tr><tr><td><a href=""{url}""style=""color: #005093; font-family: 'Nunito', sans-serif;"">{url}</a></td></tr>";

            //SendEmail(email, $"{Localizer["email_payment_initiate"].Value}", DesignEmail($"{Localizer["email_payment_initiate"].Value}", mailBody));
        }

        //       public async Task SendPaymentTransationSuccessEmail(
        //           string email,
        //           string fullName,
        //           string amount,
        //           string contractNumber,
        //           string referenceNumber,
        //           string paymentType,
        //           UpdatePaymentDbRequest updateRequest,
        //           CustomerSendEmailRequest customerSendEmailRequest)
        //       {
        //           string fullAmountText = "";
        //           int counter = 0;
        //           foreach (var item in amount.Split("."))
        //           {
        //               if (item != "00")
        //                   fullAmountText += PaymentHelpers.ConvertWholeNumber(item);
        //               if (counter == 0)
        //                   fullAmountText += " Rs ";
        //               else if (item != "00")
        //                   fullAmountText += " Paisa ";
        //               counter++;
        //           }

        //           var logoImage = $"{BaseUrlConfigs.ImageBase}{FolderEnums.customdocuments.ToString()}/email_logo.png";
        //           customerSendEmailRequest.DownloadLink = await new GenericHelpers(BaseUrlConfigs, DapperRepository).SetShortURLAsync(customerSendEmailRequest.DownloadLink);

        //           var mailBody = @$"
        //<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
        //	<tr>
        //		<td align=""center"">
        //			<table border=""0"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ffffff"" width=""100%"" align=""center"" style=""table-layout: fixed; max-width: 600px; background-color: #ffffff;"">
        //				<tr>
        //					<td height=""20"">&nbsp;</td>
        //				</tr>
        //				<tr>
        //					<td>
        //						<table border=""0"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ffffff"" width=""100%"" align=""center"" style=""border:1px solid #000;"">
        //							<tr>
        //								<td>
        //									<table border=""0"" cellpadding=""0"" cellspacing=""0"">
        //										<tr>
        //											<td style=""width:20px;"">&nbsp;</td>
        //                                               <td style=""width:80px;"" valign=""middle"">
        //                                                    <img src=""{logoImage}"" align=""logo"" height=""50"">
        //                                               </td>
        //                                               <td style=""width:500px;"">
        //                                                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" style=""width: 100%;"" width=""100%"">
        //                                                       <tr>
        //                                                           <td height=""5"" style=""font-size: 0px; line-height: 0px;""></td>
        //                                                       </tr>
        //                                                       <tr>
        //                                                           <td style=""font-family: 'Open Sans', sans-serif; text-align: center; font-size: 16px; line-height: 26px; color: #000000; font-weight: bold; text-transform: uppercase;"">The Voice LIMITED</td>
        //                                                       </tr>
        //                                                       <tr>
        //                                                           <td style=""font-family: 'Open Sans', sans-serif; text-align: center;font-size: 14px; line-height: 22px; color: #000000;"">CIN: L40200GJ2012SGC069118<br> REGD. OFFICE: The Voice CNG Station, Sector 5/C <br>Gandhinagar - 382006</td>
        //                                                       </tr>
        //                                                       <tr>
        //                                                           <td height=""5"" style=""font-size: 0px; line-height: 0px;""></td>
        //                                                       </tr>
        //                                                   </table>
        //                                               </td>
        //										</tr>
        //									</table>
        //								</td>
        //							</tr>
        //							<tr>
        //								<td style=""font-family: 'Open Sans', sans-serif; text-align: center; font-size: 14px; line-height: 22px; color: #000000; font-weight: bold; padding: 5px 0px; border: 1px solid #000000; border-width: 1px 0px;"">Payment Receipt</td>
        //							</tr>
        //							<tr>
        //								<td>
        //									<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""100%"">
        //										<tr>
        //											<td>
        //												<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""100%"" style=""border-bottom: 1px solid #000000;"">
        //													<tr>
        //														<td width=""200"" style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; border-right: 1px solid #000000; color: #000000; padding: 8px 10px;"">Payment Date</td>
        //														<td style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; color: #000000; padding: 8px 10px;"">{updateRequest?.TransactionDate ?? DateTime.Now.ToString()}</td>
        //													</tr>
        //												</table>
        //											</td>
        //										</tr>
        //										<tr>
        //											<td>
        //												<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""100%"" style=""border-bottom: 1px solid #000000;"">
        //													<tr>
        //														<td width=""200"" style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; border-right: 1px solid #000000; color: #000000; padding: 8px 10px;"">Receipt/Ref. No/Cheque No</td>
        //														<td style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; color: #000000; padding: 8px 10px;"">{referenceNumber}</td>
        //													</tr>
        //												</table>
        //											</td>
        //										</tr>
        //										<tr>
        //											<td>
        //												<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""100%"" style=""border-bottom: 1px solid #000000;"">
        //													<tr>
        //														<td width=""200"" style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; border-right: 1px solid #000000; color: #000000; padding: 8px 10px;"">Customer Id/Application No</td>
        //														<td style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; color: #000000; padding: 8px 10px;"">{contractNumber}</td>
        //													</tr>
        //												</table>
        //											</td>
        //										</tr>
        //										<tr>
        //											<td>
        //												<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""100%"" style=""border-bottom: 1px solid #000000;"">
        //													<tr>
        //														<td width=""200"" style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; border-right: 1px solid #000000; color: #000000; padding: 8px 10px;"">Customer Name</td>
        //														<td style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; color: #000000; padding: 8px 10px;"">{fullName}</td>
        //													</tr>
        //												</table>
        //											</td>
        //										</tr>
        //										<tr>
        //											<td>
        //												<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""100%"" style=""border-bottom: 1px solid #000000;"">
        //													<tr>
        //														<td width=""200"" style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; border-right: 1px solid #000000; color: #000000; padding: 8px 10px;"">Mode of Payment</td>
        //														<td style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; color: #000000; padding: 8px 10px;"">{paymentType}</td>
        //													</tr>
        //												</table>
        //											</td>
        //										</tr>
        //										<tr>
        //											<td>
        //												<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""100%"" style=""border-bottom: 1px solid #000000;"">
        //													<tr>
        //														<td width=""200"" style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; border-right: 1px solid #000000; color: #000000; padding: 10px;"">Amount (Rs.)</td>
        //														<td style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; color: #000000; padding: 10px;"">{amount}</td>
        //													</tr>
        //												</table>
        //											</td>
        //										</tr>
        //										<tr>
        //											<td>
        //												<table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""100%"" style=""border-bottom: 1px solid #000000;"">
        //													<tr>
        //														<td valign=""top"" width=""200"" style=""font-family: 'Open Sans', sans-serif; font-size: 15px; line-height: 20px;border-right: 1px solid #000000; color: #000000; padding: 10px;"">Amount (Rs.) in words</td>
        //														<td style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; color: #000000; padding: 10px;"">{fullAmountText} Only</td>
        //													</tr>
        //												</table>
        //											</td>
        //										</tr>
        //									</table>
        //								</td>
        //							</tr>
        //							<tr>
        //								<td style=""font-family: 'Open Sans', sans-serif; font-size: 14px; line-height: 20px; color: #000000; padding: 10px;"">This receipt is subject to realization of your payment. Your payment will be updated in your account within 3 working days under normal circumstances. This is a system generated receipt & does not require a signature.</td>
        //							</tr>
        //						</table>
        //					</td>
        //				</tr>
        //				<tr>
        //					<td height=""20"">&nbsp;</td>
        //				</tr>
        //			</table>
        //		</td>
        //</table>";

        //           SendEmailWithAttachment(
        //               email,
        //               $"{Localizer["email_payment_success"].Value}",
        //                DesignEmail($"{Localizer["email_payment_success"].Value}", mailBody),
        //                customerSendEmailRequest.DownloadFilePath);
        //       }

        //       public void SendPaymentTransationFailedEmail(
        //           string email,
        //           string name,
        //           string amount,
        //           string status)
        //       {
        //           var mailBody = @$"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {name},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Your payment of {amount} is {status}.</td></tr>";

        //           SendEmail(email, $"{Localizer["email_payment_failed"].Value}", DesignEmail($"{Localizer["email_payment_failed"].Value}", mailBody));
        //       }

        #endregion Send Payment Related Emails  (i.e. Initiate, Success, Failed)

        #region Customer Registration Form Mail

        public void SendCustomerRegistrationEmail(
            string email,
            string name)
        {
            //url = new GenericHelpers().MakeTinyUrl(url);
            var mailBody = @$"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {name},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Your The Voice application has been submitted Successfully. <br /> Thank you for choosing The Voice.</td></tr>";

            SendEmail(email, $"{Localizer["email_customer_form_submitted"].Value}", DesignEmail($"{Localizer["email_customer_form_submitted"].Value}", mailBody));
        }

        //public void SendCustomerRegistrationEmail(string email, string name, string url)
        //{
        //    url = new GenericHelpers().MakeTinyUrl(url);

        //    var mailBody = @$"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {name},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">You can download your registration form by clicking below button.</td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr align=""center""><td><a href=""{url}""style=""font-family: 'Nunito', sans-serif; padding: 12px; min-width: 216px; display: inline-block; background: #4cb034; border-radius: 4px; font-size: 14px; line-height: 20px; color: #fff;"">Download Registration Form</a></td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">If that doesn’t work, copy and paste the following link in your browser:</td></tr><tr><td style="" line-height: 0px;"" height=""4""></td></tr><tr><td><a href=""{url}""style=""color: #005093; font-family: 'Nunito', sans-serif;"">{url}</a></td></tr>";

        //    SendEmail(email, $"{Localizer["email_customer_form_submitted"].Value}", DesignEmail($"{Localizer["email_customer_form_submitted"].Value}", mailBody));
        //}

        //public async Task SendCustomerRegistrationEmailWithAttachment(
        //    string email,
        //    string name,
        //    CustomerSendEmailRequest customerSendEmailRequest, 
        //    string applicationNumber)
        //{
        //    customerSendEmailRequest.DownloadLink = await new GenericHelpers(BaseUrlConfigs, DapperRepository).SetShortURLAsync(customerSendEmailRequest.DownloadLink);

        //    var mailBody = @$"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {name},</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">You can download your registration form by clicking below button. <strong>Your registered application number is {applicationNumber}.</strong></td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr align=""center""><td><a href=""{customerSendEmailRequest.DownloadLink}""style=""font-family: 'Nunito', sans-serif; padding: 12px; min-width: 216px; display: inline-block; background: #4cb034; border-radius: 4px; font-size: 14px; line-height: 20px; color: #fff;"">Download Registration Form</a></td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">If that doesn’t work, copy and paste the following link in your browser:</td></tr><tr><td style="" line-height: 0px;"" height=""4""></td></tr><tr><td><a href=""{customerSendEmailRequest.DownloadLink}""style=""color: #005093; font-family: 'Nunito', sans-serif;"">{customerSendEmailRequest.DownloadLink}</a></td></tr>";

        //    SendEmailWithAttachment(
        //        email,
        //        $"{Localizer["email_customer_form_submitted"].Value}",
        //        DesignEmail($"{Localizer["email_customer_form_submitted"].Value}", mailBody),
        //        customerSendEmailRequest.DownloadFilePath);
        //}

        #endregion Customer Registration Form Mail

        //#region Send Error Logs to Authority

        //public void SendSapFileErrorLogs(
        //    string email,
        //    string type,
        //    ICollection<SapFileErrorLogsEmailResponse> responseList)
        //{
        //    var tableData = string.Empty;

        //    foreach (var response in responseList)
        //    {
        //        tableData = $@"{tableData} <tr> <td style=""padding: 8px 15px; background: #F5FAFA; border-bottom: 1px solid #E1E5E6;"">{response.FilePath}</td><td style=""padding: 8px 15px; background: #F5FAFA; border-bottom: 1px solid #E1E5E6;"">{response.Description}</td></tr>";
        //    }

        //    var mailBody = @$"<tr><td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Respected Authority,</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Find the log for Sap File Upload Errors in <b>{type}</b> Folder. Please check, modify manually and move to Input folder.</td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr><td> <table width=""100%"" border=""0"" align=""center"" style=""font-size:14px; line-height:20px;"" cellpadding=""0"" cellspacing=""0""><thead><tr><td style=""padding: 8px 15px; background: #F5FAFA; border-bottom: 1px solid #E1E5E6;"">File Path</td><td style=""padding: 8px 15px; background: #F5FAFA; border-bottom: 1px solid #E1E5E6;"">Error Description</td></tr></thead><tbody>{tableData}</tbody></table></td></tr>";

        //    SendEmail(email, $"{Localizer["email_sap_error_log"].Value}", DesignEmail($"{Localizer["email_sap_error_log"].Value}", mailBody));
        //}

        //#endregion Send Error Logs to Authority

        #region Send Email

        private void SendEmail(string email, string subject, string body) => Messages.SendEmail(email, subject, body);

        private void SendEmailWithAttachment(
            string email,
            string subject,
            string body,
            string sourceFilePath) => Messages.SendEmailWithAttachment(
            email,
            subject,
            body,
            sourceFilePath);

        #endregion Send Email

        #region Design Email

        private string DesignEmail(
            string subject,
            string body)
        {
            // Bind CSS
            var emailStyleCss = @"/* Some resets and issue fixes */#outlook a{padding: 0;}body{width: 100% !important; size-adjust: 100%; -ms-text-size-adjust: 100%; margin: 0; padding: 0; -webkit-print-color-adjust: exact !important;}table td{border-collapse: collapse;}/* End reset */html{width: 100%}html,body{margin: 0; padding: 0; height: 100% !important; width: 100% !important;}/* What it does: Stops email clients resizing small text. */*{-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;}body{width: 100%; margin: 0; padding: 0; -webkit-font-smoothing: antialiased; font-family: 'Nunito', sans-serif; -webkit-text-size-adjust: none; -ms-text-size-adjust: none}table{border-spacing: 0; border-collapse: collapse}table td{border-collapse: collapse}table tr{border-collapse: collapse}img{display: block !important}br,strong br,b br,em br,i br{line-height: 100%}a{text-decoration: none}";

            // Bind Logo
            var logoImage = $"{BaseUrlConfigs.ImageBase}{FolderEnums.games.ToString()}/email_logo.png";

            // Generate response
            var response = @$"<!DOCTYPE htmlPUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd""><html xmlns=""http://www.w3.org/1999/xhtml""><head><meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8""/><meta name=""format-detection"" content=""telephone=no""><meta name=""viewport"" content=""width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=no;""><meta http-equiv=""X-UA-Compatible"" content=""IE=9; IE=8; IE=7; IE=EDGE""/><title>The Voice</title><link href=""https://fonts.googleapis.com/css2?family=Nunito:wght@400;700&display=swap"" rel=""stylesheet""><link href=""https://fonts.googleapis.com/css2?family=Merriweather:wght@700&display=swap"" rel=""stylesheet""> <link href=""{emailStyleCss}"" type=""text/css"" rel=""stylesheet""/></head><body leftmargin=""0"" topmargin=""0"" marginwidth=""0"" marginheight=""0"" offset=""0"" bgcolor=""#f5fafa"" yahoo=""fix"" style=""margin:0; padding:0; -webkit-text-size-adjust:none; -ms-text-size-adjust:none;""><table cellspacing=""0"" cellpadding=""0"" border=""0"" width=""100%"" class=""ReadMsgBody"" align=""center""style=""font-family: 'Nunito', sans-serif;"" bgcolor=""#f5fafa"" ><tr><td><table cellspacing=""0"" cellpadding=""0"" border=""0"" width=""600"" class=""col-600"" align=""center""style="" background: #f5fafa;""><tr><td style=""line-height: 0px;"" height=""20""></td></tr><tr><td align=""center""><a href="""" target=""_blank""><img src=""{logoImage}"" height=""66px""/></a></td></tr><tr><td style="" line-height: 0px;"" height=""20""></td></tr><tr><td style=""font-size: 16px; line-height: 22px; color: #424242;""><table class=""col-600"" width=""100%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0""style="" background: #FFFFFF; border-radius: 4px;""><tr><td align=""center"" height=""4""style=""font-size: 0; line-height: 4px; background: #4cb034; border-radius: 4px 4px 0 0;""></td></tr><tr><td style=""padding: 0 24px;""><table width=""100%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0""><tr><td style="" line-height: 0px;"" height=""28""></td></tr><tr><td align=""center""style=""font-size: 24px; line-height: 23px; color: #005093; font-weight: 700; font-family: 'Merriweather', serif;"">{subject}</td></tr><tr><td style="" line-height: 0px;"" height=""24""></td></tr>{body}<tr><td style="" line-height: 0px;"" height=""32""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">Thank You.</td></tr><tr><td style=""line-height: 0px;"" height=""12""></td></tr><tr><td style=""font-family: 'Nunito', sans-serif;"">The Voice Authority,</td></tr><tr><tdstyle=""font-family: 'Nunito', sans-serif; font-size: 18px; line-height: 25px; color: #000000; font-weight: 700;""><b>The Voice</b></td></tr><tr><td style=""line-height: 0px;"" height=""24""></td></tr></table></td></tr><tr><td align=""center"" height=""4""style=""font-size: 0; line-height: 4px; background: #4cb034; border-radius: 0 0 4px 4px;"">line</td></tr></table></td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr><tr><td align=""center"" style=""font-family: 'Nunito', sans-serif;"">The Voice. All rights reserved.</td></tr><tr><td style="" line-height: 0px;"" height=""16""></td></tr></table></td></tr><tr><td style="" line-height: 0px;"" height=""24""></td></tr><tr><td style="" line-height: 0px;"" height=""32""></td></tr></table></td></tr></table></body></html>";

            return response;
        }

        #endregion Design Email

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}