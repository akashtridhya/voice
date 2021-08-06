using voice.models;
using System;

namespace voice.notify
{
    public interface IMessages : IDisposable
    {
        void SendEmail(string email, string subject, string body);

        void SendEmailWithAttachment(string email, string subject, string body, string sourceFilePath);

        System.Threading.Tasks.Task SendSms(string mobile, string body);

        void SendPush(string data, string Token, string notificationType, DeviceTypeEnums Android);
    }
}