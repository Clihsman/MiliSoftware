using MiliSoftware.UI;
using System;

namespace MiliSoftware.Views.Alerts
{
    public class MessageBoxXAction
    {
        public Action<DialogResult> action { get; private set; }
        public void Then(Action<DialogResult> action)
        {
            this.action = action;
        }
    }
}
