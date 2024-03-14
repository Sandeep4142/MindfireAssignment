using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Utility
{
    public class Logger
    {
        public static void WriteLog(Exception ex)
        {
            try
            {
                string filename = DateTime.Now.ToString("yyyy-MM-dd") + "_ErrorLog.txt";
                string logDirectory = ConfigurationManager.AppSettings["logPath"];
                string logPath = Path.Combine(logDirectory, filename);

                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine($"{DateTime.Now} | {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        writer.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
