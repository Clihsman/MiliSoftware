/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/10/2022          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware.UI;
using System;
using System.Threading.Tasks;

namespace MiliSoftware.Login.Controller
{
    public class LoginController
    {
        private ILoginGUI view { get; set; }

        private Newtonsoft.Json.Linq.JObject error;

        public LoginController(ILoginGUI view)
        {
            this.view = view;
        }

        public async Task<bool> LogIn()
        {
            WebServices.WebPostService webPostService = new WebServices.WebPostService("http://localhost:3000/api/auth/signin");
            string result = await webPostService.PostJson(new User(view.GetUserEmail(), view.GetUserPassword()));

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            Console.WriteLine(jObject);

            if (jObject.Value<int>("statusCode") == 200)
            {
                return true;
            }
            else
            {
                error = jObject;
            }

            return false;
        }

        private bool OnLogin()
        {
            /*
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
            */
            return true;
        }

        public Newtonsoft.Json.Linq.JObject GetError() {
            return error;
        }
    }
}