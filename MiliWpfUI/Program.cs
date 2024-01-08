/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 15/09/2021          *
 * Assembly : MiliWpfUI                *
 * *************************************/

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System;
using System.Runtime.InteropServices;

namespace MiliSoftware
{
    public static class Program
    {
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main(string[] args)
        {
            MiliSoftware.ControllerMain.Main(args);
            MiliWpfUI.App app = new MiliWpfUI.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
