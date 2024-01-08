using MiliSoftware.Inventario;
using System;
using Newtonsoft.Json;
using OpenRestClient.Enums;
using MiliSoftware.Inventario.Modelos;
using System.Diagnostics;
using OpenRestClient;
using System.Net;
using MiliSoftware.Acceso;
using MiliSoftware.Acceso.Modelos;
using MiliSoftware.Errores;

namespace MiliSoftware
{
    internal class Program
    {
        private static readonly AccesoApi accesoApi = new AccesoApi();
        private static readonly InventarioGrupoApi inventarioGrupoApi = new InventarioGrupoApi();
        public static void Main(string[] args) {
            /*
            AccesoToken accesoToken = accesoApi.Acceder(new Credenciales() {Correo="clihsman.cs@gmail.com",Contraseña="clihsman123457896." }).Result;
          //  Console.WriteLine(accesoToken.Token);
          
            var sp = ServicePointManager.FindServicePoint(new Uri("http://localhost:8000"));
            sp.ConnectionLeaseTimeout = 60 * 1000; // 1 minuto
            Console.WriteLine("Iniciar");
            Console.ReadKey();

            inventarioGrupoApi.AddHeader("Authorization", "Bearer " + accesoToken.Token);

            readApi();
            readApi();
            */

            ApplicationResponseType applicationResponseType = new ApplicationResponseType();
            

            Console.ReadKey();
        }

        private static void readApi() {
            Stopwatch timeMeasure = new Stopwatch();
            timeMeasure.Start();

            RestResponse<InventarioGrupo[], AccesoError> resultado = inventarioGrupoApi.All().Result;

            if (resultado)
            {
                foreach (InventarioGrupo group in resultado.Value0)
                {
                    Console.WriteLine(group.Nombre);
                }
            }
            else {
                Console.WriteLine(resultado.Value1.Estado);
            }

            timeMeasure.Stop();
            Console.WriteLine(timeMeasure.Elapsed);
        }
    }
}
