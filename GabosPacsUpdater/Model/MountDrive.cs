using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace GabosPacsUpdater.Model
{
    public class MountDrive
    {
        public static bool IsMount()
        {
            return System.IO.Directory.Exists(@"P:\");
        }

        public static void Mount()
        {
            var newProcessInfo = new System.Diagnostics.ProcessStartInfo();
            newProcessInfo.FileName = @"powershell";
            newProcessInfo.Verb = "runas";           
            newProcessInfo.Arguments = $" -Command \"{Properties.Resources.MountDriveScript}\"";

            System.Diagnostics.Process.Start(newProcessInfo).WaitForExit(5000);
        }
    }
}
