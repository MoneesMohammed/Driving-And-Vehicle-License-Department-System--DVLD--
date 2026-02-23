using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;


namespace DVLD_DataAccessLayar
{
    public class clsUtil
    {
        public static void WriteOnEventLog(string SourceName , string Message , EventLogEntryType Type , string LogName = "Application")
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, LogName);
            }

            EventLog.WriteEntry(SourceName, Message, Type);

        }


        static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }
}
