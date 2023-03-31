using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware
{
    public class languaje
    {
         public static dynamic MainWindow;

         public static void Init() {
/*
            MiliFileEngine.src.Resources.MiliResources.Externs.SetString(
              MiliFileEngine.src.Resources.ResHash.GetHash("SETTING"), Newtonsoft.Json.JsonConvert.SerializeObject(new
              {
                  language = "es-CO",
              }));
              */

            MiliFileEngine.src.Resources.MiliResources.Externs.SetString(
            MiliFileEngine.src.Resources.ResHash.GetHash("SETTING"), Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                language = "en-US",
            }));
            
            string json = MiliFileEngine.src.Resources.MiliResources.Externs.GetString(MiliFileEngine.src.Resources.ResHash.GetHash("SETTING"));
            dynamic setting = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            string language = setting.language;
            /*

            MiliFileEngine.src.Resources.MiliResources.Externs.SetString(
               MiliFileEngine.src.Resources.ResHash.GetHash("es-CO"), Newtonsoft.Json.JsonConvert.SerializeObject(new {
                   toolTipTask= "Ejecutando una tarea",
                   toolTipAccount = "Cuenta",
                   toolTipHelp = "Ayuda",
                   toolTipSetting = "Configuración"
               }));
              

            MiliFileEngine.src.Resources.MiliResources.Externs.SetString(
             MiliFileEngine.src.Resources.ResHash.GetHash("en-US"), Newtonsoft.Json.JsonConvert.SerializeObject(new
             {
                 toolTipTask = "Running a Task",
                 toolTipAccount = "Account",
                 toolTipHelp = "Help",
                 toolTipSetting = "Setting"
             }));
             */

            json = MiliFileEngine.src.Resources.MiliResources.Externs.GetString(MiliFileEngine.src.Resources.ResHash.GetHash(language));
            dynamic value = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            MainWindow = value;
        }
    }
}
