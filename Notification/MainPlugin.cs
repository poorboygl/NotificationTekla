using System;
using System.Collections.Generic;
using Tekla.Structures.Plugins;

namespace Notification
{
    [Plugin("Notification")]
    [PluginUserInterface("Notification.fNotification")]
    public class MainPlugin : PluginBase
    {
        public override List<InputDefinition> DefineInput()
        {
            return new List<InputDefinition>();
        }

        public override bool Run(List<InputDefinition> Input)
        {
            return true;
        }
    }
}
