using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System;
using System.Runtime.InteropServices;

namespace MiliSoftware
{
    public static class Program
    {
       // [StructLayout(LayoutKind.Sequential)]
        class UnitOfMeasurement
        {
            public int Unit0 { get; private set; }
            public int Unit1 { get; private set; }
            public int Unit2 { get; private set; }
            public int Unit3 { get; private set; }
            private UnitOfMeasurement Parent;

            private int this[int index]
            {
                set
                {
                    if (index == 0)
                        Unit0 = value;

                    if (index == 1)
                        Unit1 = value;

                    if (index == 2)
                        Unit2 = value;

                    if (index == 3)
                        Unit3 = value;

                }
                get
                {
                    if (index == 0)
                        return Unit0;

                    if (index == 1)
                        return Unit1;

                    if (index == 2)
                        return Unit2;

                    if (index == 3)
                        return Unit3;
                    return 0;
                }
            }

            public UnitOfMeasurement(int Unit0, int Unit1, int Unit2, int Unit)
            { 
                Parent = null;
                this.Unit0 = Unit0;
                this.Unit1 = Unit1;
                this.Unit2 = Unit2;
                this.Unit3 = Unit3;
            }

            public UnitOfMeasurement(UnitOfMeasurement Parent)
            {
                this.Parent = Parent;
                Unit0 = 0;
                Unit1 = 0;
                Unit2 = 0;
                Unit3 = 0;
            }

            public void Add(byte UnitIndex, int Unit)
            {
                this[UnitIndex] += Unit;
                UpdateValues();
            }

            public void Subtract(byte UnitIndex, int Unit)
            {
                this[UnitIndex] -= Unit;
                UpdateValues();
            }

            public int GetUnit(byte UnitIndex) {
                if (UnitIndex == 0)
                    return Unit3 * Parent.Unit2;

                if (UnitIndex == 1)
                    return Unit2 * Parent.Unit1;

                if (UnitIndex == 2)
                    return Unit1 * Parent.Unit0;

                if (UnitIndex == 3)
                    return Unit3;

                return -1;

            }

            public void UpdateValues() {

                for (int i = 0; i < 4; i++)
                {
                    if (this[i] < 0)
                    {
                        this[i] = Parent[i] + this[i];
                        this[i + 1]--;
                    }
                    if (this[i] > Parent[i] - 1)
                    {
                        this[i] = this[i] - Parent[i];
                        this[i + 1]++;
                    }
                }
                /*
                if (Unit0 < 1)
                {
                    Unit0 = Parent.Unit0 + Unit0;
                    Unit1--;
                }

                if (Unit1 < 1)
                {
                    Unit1 = Parent.Unit1 + Unit1;
                    Unit2--;
                }

                if (Unit2 < 1)
                {
                    Unit2 = Parent.Unit2 + Unit2;
                    Unit3--;
                }

                if (Unit0 > Parent.Unit0 - 1)
                {
                    Unit0 = Unit0 - Parent.Unit0;
                    Unit1++;
                }

                if (Unit1 > Parent.Unit1 - 1)
                {
                    Unit1 = Unit1 - Parent.Unit1;
                    Unit2++;
                }

                if (Unit2 > Parent.Unit2 - 1)
                {
                    Unit2 = Unit2 - Parent.Unit2;
                    Unit3++;
                }
                */
            }

            public override string ToString()
            {
                return string.Format("V0: {0}, V1: {1}, V2: {2}, V3: {3}", Unit0 ,Unit1, Unit2, Unit3);
            }
        }

        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main(string[] args)
        {
            //    Console.WriteLine(Temperature.Temperatures[0]);
            /*
            string accountSid = "ACd6800830749382d47e57aac9fc00a49e";
            string authToken = "25f38c4ae5f95bfe728c8725607cdbb1";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Codigo de verificacion\nM-141541",
                from: new Twilio.Types.PhoneNumber("+15077086414"),
                to: new Twilio.Types.PhoneNumber("+573138708901")
            );
            */
            Random r = new Random();
            UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement(new UnitOfMeasurement(1000, 1000, 1000, 1000));
            unitOfMeasurement.Add(3, 5);
            unitOfMeasurement.Add(2, 5);
            while (!true)
            {
                Console.WriteLine("{0}kg, {1}g", unitOfMeasurement.Unit3, unitOfMeasurement.Unit2);
                Console.WriteLine("{0}mg", unitOfMeasurement.GetUnit(1));
                ConsoleKey key = Console.ReadKey(true).Key;
                if(key == ConsoleKey.UpArrow)
                    unitOfMeasurement.Add(2, 1);
                if (key == ConsoleKey.DownArrow)
                    unitOfMeasurement.Subtract(2, 1);
            }
           // MiliSoftware.ControllerMain.Main(args);
            //   Console.ReadKey();
            
            MiliWpfUI.App app = new MiliWpfUI.App();
            app.InitializeComponent();
            app.Run();
            
        }
    }
}
