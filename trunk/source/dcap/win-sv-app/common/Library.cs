using System;
using System.IO;

namespace win_sv_app.common
{
    public static class Library
    {
        public static void WriteErrorLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {   
            }
        }
    }
}
