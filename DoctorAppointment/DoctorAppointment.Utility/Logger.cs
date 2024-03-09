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
                string logPath = ConfigurationManager.AppSettings["logPath"] + filename;
                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine($"{DateTime.Now} | {ex.Message}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
