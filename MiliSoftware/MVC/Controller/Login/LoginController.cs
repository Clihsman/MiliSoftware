/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/10/2022          *
 * *************************************/

using MiliSoftware.Login.View;
using MiliSoftware.Login.Model;
using System;

namespace MiliSoftware.Login.Controller
{
    public class LoginController
    {
        private PgLogin view;
        private LoginModel model;

        public LoginController(PgLogin view, LoginModel model)
        {
            this.view = view;
            this.model = model;

            view.onLogin += OnLogin;
        }

        private bool OnLogin(string name,string password) {
            model.SetName(name);
            model.SetPassword(password);
            return  model.Login();
        }
    }
}