using System.IO;
using System;
using ChadDotNet.Var;
using ChadDotNet.Network;

namespace ChadDotNet.Etc
{
    internal static class Log
    {
        internal static void Write(string message)
        {
            if (setup == true)
            {
                File.AppendAllText(path, message);
            }
        }

        internal static void Setup()
        {
            setup = false;
            try
            {
                path = SystemVariables.UserConfig.Path + DateTime.Now + ".txt";
                File.Create(path);
                File.AppendAllText(path, "Log of robot on " + DateTime.Now + ".");
                setup = true;
            }

            catch (Exception e)
            {
                SmartConsole.PrintError("Failed to create log file, exeption " + e.Message + " occured", e.StackTrace);
            }
        }

        private static bool setup;
        private static string path;
    }
}
