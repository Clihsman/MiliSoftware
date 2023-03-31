using MiliSoftware.Model.Security;
using System.Collections.Generic;
using System.Management;

namespace MiliSoftware.MVC.Model.Security
{
    internal class Hadware
    {
        private static string[] GetProcessorID()
        {
            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            List<string> listProcessor = new List<string>();
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                listProcessor.Add(wmi_HD["ProcessorID"].ToString().Trim());
            }
            searcher.Dispose();
            return listProcessor.ToArray();
        }

        private static string[] GetBoardID()
        {
            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            List<string> listboards = new List<string>();
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                listboards.Add(wmi_HD["SerialNumber"].ToString().Trim());
            }
            searcher.Dispose();
            return listboards.ToArray();
        }

        private static string[] GetPhysicalMemoryID()
        {
            ConnectionOptions connection = new ConnectionOptions();
            connection.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope scope = new ManagementScope("\\root\\CIMV2", connection);
            scope.Connect();
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            List<string> physicalMemories = new List<string>();
            foreach (ManagementObject queryObj in searcher.Get())
            {
                foreach (PropertyData data in queryObj.Properties)
                {
                    if (data.Name == "SerialNumber")
                        physicalMemories.Add(data.Value.ToString().Trim());
                }
            }
            searcher.Dispose();
            return physicalMemories.ToArray();
        }

        private static string[] GetPhysicalMediaID()
        {
            ConnectionOptions connection = new ConnectionOptions();
            connection.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope scope = new ManagementScope("\\root\\CIMV2", connection);
            scope.Connect();
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMedia");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            List<string> physicalMemories = new List<string>();
            foreach (ManagementObject queryObj in searcher.Get())
            {
                foreach (PropertyData data in queryObj.Properties)
                {
                    if (data.Name == "SerialNumber")
                        physicalMemories.Add(data.Value.ToString().Trim());
                }
            }
            searcher.Dispose();
            return physicalMemories.ToArray();
        }

        public static string GetHash()
        {
            string code = string.Join("", GetProcessorID());
            code += string.Join("", GetBoardID());
            code += string.Join("", GetPhysicalMemoryID());
            code += string.Join("", GetPhysicalMediaID());
            return Encrypt.GetMD5(code).ToUpper();
        }
    }
}
