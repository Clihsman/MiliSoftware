/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 02/01/2023          *
 * Assembly : MiliController            *
 * *************************************/

using GrapInterCom.Interfaces.Email;
using MilISoftware.Email;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MiliSoftware.Email
{
    public class EmailController : IController<string, object[], string, string>
    {
        private SmtpServices email;
        private IEmailGUI emailGUI;

        public EmailController(IEmailGUI emailGUI) {
            this.emailGUI = emailGUI;
            emailGUI.controller = this;
            emailGUI.Show();
        }

        private SecureSocketOptions GetSocketOptions(string type)
        {
            if (type == "ssl") return SecureSocketOptions.SslOnConnect;
            if (type == "tls") return SecureSocketOptions.StartTls;
            if (type == "tlsw") return SecureSocketOptions.StartTlsWhenAvailable;
            if (type == "auto") return SecureSocketOptions.Auto;
            return SecureSocketOptions.None;
        }

        public bool Create(string data)
        {
            var dataDic = JsonConvert.DeserializeObject<Dictionary<string,object>>(data);
            dynamic serverInfo = new DObject(dataDic);
            email = new SmtpServices();

            if (email.Connect(serverInfo.Host, (int)serverInfo.Port, serverInfo.UserName, serverInfo.Passworld, GetSocketOptions(serverInfo.SecureSocket)))
            {
                if (!email.SendMessages(emailGUI.GetFrom(), new string[] { emailGUI.GetTo() }, emailGUI.GetSubject(), emailGUI.GetBody(), emailGUI.GetFiles()))
                {
                    email.Disconnect();
                    throw new MiliConException(0x4, CodeException.MailCouldNotBeSent, "Mail could not be sent");
                }
                email.Disconnect();
            }
            else
            {
                throw new MiliConException(0x4, CodeException.CouldNotConnectToTheServer, "Could not connect to the server");
            }
            
            return true;
        }

        public bool Delete(string data)
        {
            throw new System.NotImplementedException();
        }

        public object[] Read(object[] data)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}
