using System;
using System.Diagnostics;


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
    }
}
