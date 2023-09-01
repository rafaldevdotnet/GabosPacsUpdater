using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
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
                status = ex.Message;
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
                sc.WaitForStatus(ServiceControllerStatus.Running);
                status = sc.Status.ToString();
                return true;
            }
            catch (Exception ex)
            {
                status = ex.Message;
                return false;
            }
        }

        public static bool StopServices(string serviceName, out string status)
        {
            try
            {
                ServiceController sc = new ServiceController();
                sc.ServiceName = serviceName;

                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                status = sc.Status.ToString();
                return true;
            }
            catch (Exception ex)
            {
                status = ex.Message;
                return false;
            }
        }

    }
}
