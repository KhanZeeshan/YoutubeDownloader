using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace YoutubeDownloader
{
    class Common
    {
        public static void LogError(Exception ex)
        {
            if (!ex.Message.Contains("Thread was being aborted"))
            {
                string LogFilePath = Application.StartupPath + @"\Log\";
                if (Directory.Exists(LogFilePath) == false)
                    Directory.CreateDirectory(LogFilePath);

                if (ex.InnerException != null)
                    File.WriteAllText(LogFilePath + "Error.log", "Date And Time : " + DateTime.Now.ToString() + Environment.NewLine +
                        "Message: " + ex.Message + Environment.NewLine +
                        "Stack Trace: " + ex.StackTrace + Environment.NewLine + "Inner Exception: " +
                        Environment.NewLine + ex.InnerException);

                else
                    File.WriteAllText(LogFilePath + "Error.log",
                        "Date And Time : " + DateTime.Now.ToString() + Environment.NewLine +
                        "Message: " + ex.Message + Environment.NewLine +
                    "Stack Trace: " + ex.StackTrace);
            }
        }

        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public static string RemoveCharaters(string Text)
        {
            string NewString = string.Empty;
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] != '\\' && Text[i] != '/' && Text[i] != '|'
                    && Text[i] != ':' && Text[i] != '*' && Text[i] != '?' && Text[i] != '"' && Text[i] != '<'
                    && Text[i] != '>')
                {
                    NewString += Text[i];
                    continue;
                }
            }
            return NewString;
        }

        public static bool CheckStringForValue(ref string StringToCheck, string[] ArrayContainingValuetoCheck)
        {
            bool RetVal = false;
            if (StringToCheck != null && StringToCheck.Trim().Length > 0
                && ArrayContainingValuetoCheck != null && ArrayContainingValuetoCheck.Length > 0)
            {
                for (int i = 0; i < ArrayContainingValuetoCheck.Length; i++)
                {
                    if (StringToCheck.Contains(ArrayContainingValuetoCheck[i].Trim()))
                    {
                        RetVal = true;
                        break;
                    }
                    else
                    {
                        RetVal = false;
                        continue;
                    }
                }
            }
            return RetVal;
        }
    }
}
