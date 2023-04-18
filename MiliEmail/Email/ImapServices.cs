/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 02/01/2023          *
 * Assembly : MiliEmail                *
 * *************************************/

using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System.Collections.Generic;
using System;

namespace MilISoftware.Email
{
    public class ImapServices : IDisposable
    {
        private MimeMessage[] messages;
        private List<Dictionary<string, object>> emails;
        private ImapClient imapClient;
        private bool disposed;

        public bool Connect(string host, int port, string userName, string password, SecureSocketOptions secureSocket) {
            try
            {
                imapClient = new ImapClient();
                imapClient.Connect(host, port, (MailKit.Security.SecureSocketOptions)secureSocket);
                imapClient.Authenticate(userName, password);
                return true;
            }
            catch { }
            return false;
        }

        public Dictionary<string,object>[] GetEmails()
        {
            emails = new List<Dictionary<string, object>>();
            int id = 0;
            foreach (MimeMessage message in DownloadMessages())
            {
                string from = message.From[0].ToString();
                int index = from.LastIndexOf(" ");
                string email;
                string name;

                if (index > 0)
                    email = from.Substring(index, from.Length - index).Replace("<", "").Replace(">", "");
                else
                    email = from;

                if (index > 0)
                    name = from.Substring(0, index).Replace("\"", "");
                else
                    name = from;

                string subject = message.Subject;

                emails.Add(new Dictionary<string, object>
                {
                    { "Name", name },
                    { "Email", email },
                    { "Subject", subject },
                    { "Date", message.Date.DateTime.ToString()},
                    { "Id", id }
                });
                id++;
            }

            return emails.ToArray();
        }

        public Dictionary<string, object>[] GetSendEmails()
        {
            emails = new List<Dictionary<string, object>>();
            int id = 0;
            foreach (MimeMessage message in DownloadSendMessages())
            {
                string from = message.To[0].ToString();
                int index = from.LastIndexOf(" ");
                string email;
                string name;

                if (index > 0)
                    email = from.Substring(index, from.Length - index).Replace("<", "").Replace(">", "");
                else
                    email = from;

                if (index > 0)
                    name = from.Substring(0, index).Replace("\"", "");
                else
                    name = from;

                string subject = message.Subject;

                emails.Add(new Dictionary<string, object>
                {
                    { "Name", name },
                    { "Email", email },
                    { "Subject", subject },
                    { "Date", message.Date.DateTime.ToString()},
                    { "Id", id }
                });
                id++;
            }

            return emails.ToArray();
        }

        public Dictionary<string, object> GetMenssage(int id)
        {
            MimeMessage message = messages[id];

            string from = message.From[0].ToString().Replace("\"", "");
            string date = message.Date.ToString();
            string subject = message.Subject;
            string body;

            if (string.IsNullOrWhiteSpace(message.HtmlBody))
                body = "<h4>" + message.TextBody + "</h4>";
            else
                body = message.HtmlBody;

            bool isAttachment = message.Body.IsAttachment;

            return new Dictionary<string, object>{
                { "From", from },
                { "Date", date },
                { "Subject", subject },
                { "Body", body },
                { "IsAttachment", isAttachment }
            };
        }

        public Dictionary<string, object> GetSendMenssage(int id)
        {
            MimeMessage message = messages[id];

            string from = message.To[0].ToString().Replace("\"", "");
            string date = message.Date.ToString();
            string subject = message.Subject;
            string body;

            if (string.IsNullOrWhiteSpace(message.HtmlBody))
                body = "<h4>" + message.TextBody + "</h4>";
            else
                body = message.HtmlBody;

            bool isAttachment = message.Body.IsAttachment;

            return new Dictionary<string, object>{
                { "From", from },
                { "Date", date },
                { "Subject", subject },
                { "Body", body },
                { "IsAttachment", isAttachment }
            };
        }

        private MimeMessage[] DownloadMessages()
        {
            Stack<MimeMessage>  messages = new Stack<MimeMessage>();
            imapClient.Inbox.Open(FolderAccess.ReadOnly);
            var uids = imapClient.Inbox.Search(SearchQuery.All);

            foreach (var uid in uids)
            {
                var message = imapClient.Inbox.GetMessage(uid);
                messages.Push(message);
            }

            imapClient.Inbox.Close();

            this.messages = messages.ToArray();
            return this.messages;
        }

        private MimeMessage[] DownloadSendMessages()
        {
            Stack<MimeMessage> messages = new Stack<MimeMessage>();

            if (imapClient.Capabilities.HasFlag(ImapCapabilities.SpecialUse))
            {
                var sent = imapClient.GetFolder(SpecialFolder.Sent);
                sent.Open(FolderAccess.ReadOnly);
                var uids = sent.Search(SearchQuery.All);

                foreach (var uid in uids)
                {
                    var message = sent.GetMessage(uid);
                    messages.Push(message);
                }
                sent.Close();
            }
            else
            {
                var personal = imapClient.GetFolder(imapClient.PersonalNamespaces[0]);
                var sent = personal.GetSubfolder("Sent Items");
                sent.Open(FolderAccess.ReadOnly);
                var uids = sent.Search(SearchQuery.All);

                foreach (var uid in uids)
                {
                    var message = sent.GetMessage(uid);
                    messages.Push(message);
                }
                sent.Close();
            }

            this.messages = messages.ToArray();
            return this.messages;
        }

        public void Disconnect() {
            messages = null;
            emails = null;
            imapClient.Disconnect(true);
            imapClient.Dispose();
            imapClient = null;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                messages = null;
                emails = null;
                imapClient.Dispose();
                imapClient = null;
                disposed = true;
             }
        }
    }
}
