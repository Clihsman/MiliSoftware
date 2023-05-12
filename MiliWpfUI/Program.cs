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
