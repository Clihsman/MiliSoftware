/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/10/2022          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware.Acceso;
using MiliSoftware.UI;
using MiliSoftware.WebServices;
using MiliWebService.WebServices;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MiliSoftware.Login.Controller
{
    public class LoginController
    {
        private ILoginGUI View { get; set; }

        public LoginController(ILoginGUI view)
        {
            View = view;
        }

        public async Task<string> LogIn()
        {
            AccesoApi accesoApi = new AccesoApi();
            var acceso = await accesoApi.Acceder(new Acceso.Modelos.Credenciales(View.GetUserEmail(), View.GetUserPassword()));

            if (accesoApi.StatusCode == System.Net.HttpStatusCode.NotFound) return "not_found";
            if (accesoApi.StatusCode == System.Net.HttpStatusCode.InternalServerError) return "not_found";
            if (accesoApi.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable) return "unable_to_connect_to_remote_server";

            if (accesoApi.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                AppStaticStore.SetToken(acceso.Token);
                return acceso.Token;
            }
            return acceso.Error;
        }
    }
}