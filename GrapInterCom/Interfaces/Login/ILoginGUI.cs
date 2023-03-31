using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.UI
{
    public interface ILoginGUI : IGUI<string, object[], string, string>
    {
        object[] GetValues();
        void SetValues(object[] datas);

        string GetUserEmail();
        string GetUserPassword();

        event Func<bool> OnLogin;
    }
}
