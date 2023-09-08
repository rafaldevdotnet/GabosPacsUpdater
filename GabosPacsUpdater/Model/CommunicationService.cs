using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GabosPacsUpdater.Model
{
    public class CommunicationService
    {
        public static bool CheckStatusServices(string serviceName, out string status)
        {
			try
			{
                ServiceController sc = new ServiceController();
                sc.ServiceName = serviceName;

                status = sc.Status.ToString();
                return true;
            }
			catch (Exception ex)
			{
                status = $"Error: {ex.Message}";
                return false;
			}
        }

        public static bool StartServices(string serviceName, out string status)
        {
            try
            {
                ServiceController sc = new ServiceController();
                sc.ServiceName = serviceName;
                

                sc.Start();
                TimeSpan timeSpan = TimeSpan.FromMilliseconds(10000);
                sc.WaitForStatus(ServiceControllerStatus.Running, timeSpan);
                status = sc.Status.ToString();
                return true;
            }
            catch (Exception ex)
            {
                status = $"Error: {ex.Message}";
                return false;
            }
        }

        public static bool StopServices(string serviceName, out string status)
        {
            try
            {
                ServiceController sc = new ServiceController();
                sc.ServiceName = serviceName;

                TimeSpan timeSpan = TimeSpan.FromMilliseconds(10000);
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, timeSpan);
                status = sc.Status.ToString();
                return true;
            }
            catch (Exception ex)
            {
                status = $"Error: {ex.Message}";
                return false;
            }
        }

        public static bool InstallServices(string serviceName, string path, out string status)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo(path);
                info.Arguments = "install";
                info.UseShellExecute = true;
                info.Verb = "runas";
                Process.Start(info).WaitForExit();
                Thread.Sleep(3000);

                ServiceController sc = new ServiceController();
                sc.ServiceName = serviceName;

                status = sc.Status.ToString();
                return true;
            }
            catch (Exception ex)
            {
                status = $"Error: {ex.Message}";
                return false;
            }
        }

    }
}
