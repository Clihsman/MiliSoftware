/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 01/01/2023          *
 * Assembly : GrapInterCom             *
 * *************************************/


using GrapInterCom.Interfaces.Email;

namespace MiliSoftware.Email
{
    public class EmailViewController : IController<string, object[], string, string>
    {
        public EmailViewController(IEmailViewGUI emailView) {
            emailView.controller = this;
            emailView.Show();
        }

        public bool Create(string data)
        {
            throw new System.NotImplementedException();
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
