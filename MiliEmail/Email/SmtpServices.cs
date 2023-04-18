/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 02/01/2023          *
 * Assembly : MiliEmail                *
 * *************************************/

using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System;

namespace MilISoftware.Email
{
    public class SmtpServices : IDisposable
    {
        private SmtpClient smtpClient;

        public bool Connect(string host,int port,string userName,string password, SecureSocketOptions secureSocket) {
            try
            {
                smtpClient = new SmtpClient();
                smtpClient.Connect(host, port, (MailKit.Security.SecureSocketOptions)secureSocket);
                smtpClient.Authenticate(userName, password);
            }
            catch {
                return false;
            }

            return true;
        }

        public bool SendMessages(string from, string[] to, string subject, string body, string[] files)
        {
            List<MimeMessage> messages = new List<MimeMessage>();

            MimeMessage message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("milisoftware.asc@gmail.com"));
            foreach (string email in to)
            {
                try
                {
                    message.To.Add(MailboxAddress.Parse(email));
                }
                catch
                {
                    return false;
                }
            }
            message.Subject = subject;
            message.Body = GetMineEnity(body, files);
            messages.Add(message);
            return SendMessages(messages);
        }

        private bool SendMessages(IList<MimeMessage> messages)
        {
            foreach (var message in messages)
            {
                try
                {
                    smtpClient.Send(message);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        private MimeEntity GetMineEnity(string content, string[] files)
        {
            var builder = new BodyBuilder();
            builder.TextBody = content;

            if (files != null)
            {
                foreach (string file in files)
                    builder.Attachments.Add(file);
            }

            return builder.ToMessageBody();
        }

        public void Disconnect() {
            smtpClient.Disconnect(true);
            smtpClient.Dispose();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                if (smtpClient != null) smtpClient.Dispose();
            }
        }

    }
}
