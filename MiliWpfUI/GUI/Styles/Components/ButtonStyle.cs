using System.Windows.Media;

namespace MiliSoftware.GUI.Styles.Components
{
    public class ButtonStyle : Component
    {
        public SolidColorBrush GetForegroundBrush() {
           return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Foreground));
        }

        public SolidColorBrush GetBackgroundBrush()
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Background));
        }

        public Color GetForegroundColor()
        {
            return (Color)ColorConverter.ConvertFromString(Foreground);
        }

        public Color GetBackgroundColor()
        {
            return (Color)ColorConverter.ConvertFromString(Background);
        }
    }
}
