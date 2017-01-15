using System;
using WPILib;
using ChadDotNet.Var;
using ChadDotNet.Etc;

namespace ChadDotNet.Network
{
    public class SmartConsole
    {
        internal static void Init()
        {
            if (SystemVariables.UserConfig.Log == true)
            {
                log = true;
                Log.Setup();
                PrintInfo("Logging is enabled");
                PrintInfo("Writing to the log will NOT be reported, as this would cause infinite recursion.");
            }

            else
            {
                log = false;
            }
        }

        public static void PrintError(string error, string stacktrace)
        {
            DriverStation.ReportError(error, true);

            if (log == true)
            {
                Log.Write("ERROR: " + error + " AT: " +stacktrace);
            }
        }

        public static void PrintInfo(string info)
        {
            Console.WriteLine("INFO: " + info);

            if (log == true)
            {
                Log.Write("INFO: " + info);
            }
        }

        public static void PrintRaw(string raw)
        {
            Console.WriteLine(raw);

            if (log == true)
            {
                Log.Write(raw);
            }
        }

        public static void PrintWarning(string warning)
        {
            DriverStation.ReportWarning(warning, false);

            if (log == true)
            {
                Log.Write("WARNING: " + warning);
            }
        }

        public static void PrintVerbose(string message)
        {
            Console.WriteLine("VERBOSE: " + message);

            if (log == true)
            {
                Log.Write("VERBOSE: " + message);
            }
        }

        internal static void PrintExternal(string message)
        {
            Console.WriteLine("EXTERNAL: " + message);

            if (log == true)
            {
                Log.Write("EXTERNAL: " + message);
            }
        }

        private static bool log;
    }
}