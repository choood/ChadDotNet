using ChadDotNet.Etc;
using ChadDotNet.Network;
using ChadDotNet.Var;
using HAL.Base;
using System;
using System.IO;
using WPILib;
using static HAL.Base.HAL;

namespace ChadDotNet.System
{
    class ClassLoader
    {
        private static DriverStation driverstation;
        private static AbstractRobot robot;
        private static Report report;

        public static void Main(Config config)
        {
            SystemVariables.UserConfig = config;
            SmartConsole.PrintInfo("Starting user program using " + SystemVariables.UserConfig.Version);

            for (
                int length = 0; 
                length <= SystemVariables.UserConfig.Splash.Length; 
                length++
                )
            {
                SmartConsole.PrintRaw(SystemVariables.UserConfig.Splash[length]);
            }

            SmartConsole.Init();

            if (SystemVariables.UserConfig.Verbose == true)
            {
                SmartConsole.PrintWarning("Verbose mode is enabled, use only for debugging as this slams your RIO");
            }

            report = new Report();
            report.Verbose = "Report module for classloader online";
            SmartConsole.PrintInfo("Initialising HAL");

            try
            {
                report.Verbose = "Attempting init method...";
                Initialize();
                report.Verbose = "Init sucsesful";
                report.Verbose = "Attempting resource report...";
                Report(ResourceType.kResourceType_Language, Instances.kLanguage_DotNet);
                report.Verbose = "HAL reporting online";
                report.Verbose = "Getting driverstation instance...";
                driverstation = DriverStation.Instance;
                report.Verbose = "Sucsesful";
            }

            catch (Exception exeption)
            {
                SmartConsole.PrintError("HAL failed to start: " + exeption.Message, exeption.StackTrace);
                Environment.Exit(0);
            }

            SmartConsole.PrintInfo("Hal started sucsesfully");
            report.Verbose = "Checking HAL type";

            if (true) //TODO: @thad plz
            {
                SmartConsole.PrintInfo("Writing LIB version");

                try
                {
                    string file = "/tmp/frc_versions/FRC_Lib_Version.ini";

                    report.Verbose = "Checking file";
                    if (File.Exists(file))
                    {
                        report.Verbose = "File exists, deleting...";
                        File.Delete(file);
                        report.Verbose = "File deleted";
                    }

                    report.Verbose = "Writing to file...";
                    File.WriteAllText(file, SystemVariables.UserConfig.Version);
                    report.Verbose = "Write sucsesful";
                    SmartConsole.PrintInfo("LIB version sucsesfully written to '" + file + "'");
                }

                catch (Exception exeption)
                {
                    SmartConsole.PrintError("Failed to write library version: " + exeption.Message, exeption.StackTrace);
                }
            }

            SmartConsole.PrintInfo("Creating instance of user program from pointer");
            report.Verbose = "Checking pointer...";

            if (SystemVariables.UserConfig.RobotClass != null)
            {
                report.Verbose = "Pointer is not null";
                report.Verbose = "Creating instance of pointer";
                robot = (AbstractRobot) Activator.CreateInstance(SystemVariables.UserConfig.RobotClass);
                SmartConsole.PrintInfo("Created instance of user program");
            }

            else
            {
                SmartConsole.PrintError("Pointer can not be null", Environment.StackTrace);
                Environment.Exit(0);
            }

            SmartConsole.PrintInfo("Starting NetworkTables");

            try
            {
                robot.InitNetworkTables();
            }

            catch (Exception exeption)
            {
                SmartConsole.PrintError("Failed to start networktables: " + exeption.Message, exeption.StackTrace);
            }

            SmartConsole.PrintInfo("Networktables sucssesfuly started");
            SmartConsole.PrintInfo("Program preinitialised in " + Environment.TickCount + " milliseconds");
            bool iscrashed = false;

            try
            {
                SmartConsole.PrintInfo("User code starting");
                report.Verbose = "Running preinit...";
                robot.PreInit(driverstation);
                report.Verbose = "preinit sucsesful";
                report.Verbose = "Starting robot code";
                robot.ProgramLoop();
            }

            catch (Exception exeption)
            {
                iscrashed = true;
                SmartConsole.PrintError("User program has crashed: " + exeption.Message, exeption.StackTrace);
            }

            finally
            {
                if (iscrashed == false)
                {
                    SmartConsole.PrintError("User code has dropped from loop", Environment.StackTrace);
                }
            }
        }
    }
}