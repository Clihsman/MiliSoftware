/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 01/01/2023          *
 * Assembly : GrapInterCom             *
 * *************************************/
 
using GrapInterCom.Interfaces.Email;
using MilISoftware.Email;
using System;

namespace MiliSoftware.Email
{
    public class EmailsController : IController<string, object[], string, string>
    {
        private ImapServices imapServices;

        public EmailsController(IEmailsGUI emailGUI) {
            emailGUI.controller = this;
            emailGUI.Show();
        }

        public bool Create(string data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string data)
        {
            throw new NotImplementedException();
        }

        public void Connect(string host, int port, string userName, string password, string secure)
        {
            imapServices = new ImapServices();
            imapServices.Connect(host, port, userName, password, GetSocketOptions(secure));
        }

        public void Disconnect()
           => imapServices.Disconnect();

        public object[] Read(object[] data)
        {
            throw new NotImplementedException();

            if (data.Length == 1)
                return new DObject[] {new DObject(imapServices.GetMenssage((int)data[0]))};

            DObject[] emails = null;
            if (imapServices.Connect((string)data[0], (int)data[1], (string)data[2], (string)data[3], GetSocketOptions((string)data[4])))
            {
                emails = DObject.GetArray(imapServices.GetEmails());
                imapServices.Disconnect();
            }
            return emails;
        }

        public object[] GetSendEmails()
        {
            DObject[] emails = null;
            emails = DObject.GetArray(imapServices.GetSendEmails());
            return emails;
        }

        public object[] GetReceivedEmails()
        {
            DObject[] emails = null;
            emails = DObject.GetArray(imapServices.GetEmails());
            return emails;
        }

        public object[] GetEmails(string type)
        {
            if (type == "received")
                return GetReceivedEmails();

            if (type == "sent")
                return GetSendEmails();

            return null;
        }

        public object GetMenssage(int Id)
        {
            return new DObject(imapServices.GetMenssage(Id));
        }

        public object GetMenssage(string type, int Id)
        {
            if (type == "received")
                return GetMenssage(Id);
            if (type == "sent")
                return GetSendMenssage(Id);
            return null;
        }

        public object GetSendMenssage(int Id)
        {
            return new DObject(imapServices.GetSendMenssage(Id));
        }

        public bool Update(string data)
        {
            throw new NotImplementedException();
        }

        private SecureSocketOptions GetSocketOptions(string type)
        {
            if (type == "ssl") return SecureSocketOptions.SslOnConnect;
            if (type == "tls") return SecureSocketOptions.StartTls;
            if (type == "tlsw") return SecureSocketOptions.StartTlsWhenAvailable;
            if (type == "auto") return SecureSocketOptions.Auto;
            return SecureSocketOptions.None;
        }
    }
}
