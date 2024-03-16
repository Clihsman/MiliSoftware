/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 06/12/2023          *
 * Assembly : MiliController           *
 * *************************************/

using GrapInterCom.Interfaces.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.Setting
{
    public class SettingController
    {
        private ISettingGUI SettingGUI { get; set; }

        public SettingController(ISettingGUI settingGUI) 
        {
            SettingGUI = settingGUI;
            SettingGUI.Show();
        }
    }
}
