/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/10/2022          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware.UI;
using System;

namespace MiliSoftware.Login.Controller
{
    public class LoginController
    {
        private ILoginGUI view;

        private Newtonsoft.Json.Linq.JObject error;

        public LoginController(ILoginGUI view)
        {
            this.view = view;
            view.OnLogin += OnLogin;
           // OnLogin();
        }

        private bool OnLogin()
        {
            error = null;
            WebServices.WebPostService webPostService = new WebServices.WebPostService("http://localhost:3000/api/auth/signin");
            string result = webPostService.PostJson(new {
                email= view.GetUserEmail(),
                password= view.GetUserPassword()
            });

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            if (jObject.Value<int>("statusCode") == 200)
            {
                return true;
            }
            else
            {
                error = jObject;
                return false;
            }
        }

        public Newtonsoft.Json.Linq.JObject GetError() {
            return error;
        }
    }
}