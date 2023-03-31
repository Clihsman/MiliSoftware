using MiliSoftware.GUI.Styles.Components;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace MiliSoftware.GUI.Styles
{
    public class StyleSheet
    {
        public Dictionary<string, FormStyle> forms;

        public StyleSheet()
        {
            forms = new Dictionary<string, FormStyle>();
        }

        public FormStyle GetFormStyle(string name)
        {
            FormStyle formStyle = null;
            forms.TryGetValue(name, out formStyle);
            return formStyle;
        }

        public static StyleSheet Load(string filename) {
          return JsonConvert.DeserializeObject<StyleSheet>(File.ReadAllText(filename));
        }
    }
}
