using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.GUI.Styles.Components
{
    public class FormStyle : Component
    {
        public Dictionary<string, Component> components;

        public FormStyle() {
            components = new Dictionary<string, Component>();
        }

        public ButtonStyle GetButtonStyle(string name)
        {
            Component component = null;
            components.TryGetValue(name, out component);
            if (component != null)
                return new ButtonStyle() {Background= component .Background,Foreground= component .Foreground};
            return null;
        }
    }
}
