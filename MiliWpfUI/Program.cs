namespace MiliSoftware
{
    public static class Program
    {
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main(string[] args)
        {
        //    Console.WriteLine(Temperature.Temperatures[0]);

            MiliSoftware.ControllerMain.Main(args);
            //   Console.ReadKey();
        
            MiliWpfUI.App app = new MiliWpfUI.App();
            app.InitializeComponent();
            app.Run();
            
        }
    }
}
