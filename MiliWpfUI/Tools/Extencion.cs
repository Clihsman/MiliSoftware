using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MiliSoftware
{
    public static class Extencion
    {
        public static Collection<UIElement> Where(this UIElementCollection collection, Func<UIElement, bool> func) { 
            Collection<UIElement> result = new Collection<UIElement>();
            foreach (UIElement uIElement in collection) {
                if (func(uIElement)) { 
                    result.Add(uIElement);
                }
            }
            return result;
        }

        public static Collection<OUT> Select<IN, OUT>(this IList list, Func<IN, OUT> func)
        {
            Collection<OUT> result = new Collection<OUT>();
            foreach (object o in list) {
                result.Add(func.Invoke((IN)o));
            }
            return result;
        }

        public static Collection<T> Where<T>(this UIElementCollection collection, Func<T, bool> func) where T : UIElement
        {
            Collection<T> result = new Collection<T>();
            foreach (T uIElement in collection)
            {
                if (func(uIElement))
                {
                    result.Add(uIElement);
                }
            }
            return result;
        }

        public static void Invoke(this Page page, Action action)
        {
            page.Dispatcher.Invoke(action);
        }

        public static void Invoke(this Page page, Action action, params object[] args)
        {
            page.Dispatcher.Invoke(action, args);
        }

        public static void Invoke(this Page page,TimeSpan time, Action action)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(delegate {
                page.Dispatcher.Invoke(action);
                dispatcherTimer.Stop();
            });
            dispatcherTimer.Interval = time;
            dispatcherTimer.Start();
        }

        public static void Invoke(this Page page, TimeSpan time, Action action, params object[] args)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(delegate {
                page.Dispatcher.Invoke(action, args);
                dispatcherTimer.Stop();
            });
            dispatcherTimer.Interval = time;
            dispatcherTimer.Start();
        }

        #region String

        public static string SetUpperPrimaryChars(this string value)
        {
            string[] currentValue = value.Split(' ');
            for (int i = 0; i < currentValue.Length; i++) if (currentValue[i].Length > 0 && char.IsLetter(currentValue[i][0]))
            {
                    string char_c = currentValue[i][0].ToString().ToUpper();
                    currentValue[i] = char_c + currentValue[i].Substring(1);
            }
            return string.Join(" ", currentValue);
        }

        #endregion
    }
}
