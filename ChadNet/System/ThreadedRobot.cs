using ChadDotNet.Network;
using HAL.Base;
using NetworkTables;
using System;
using System.Threading;
using static HAL.Base.HAL;

namespace ChadDotNet.System
{
    public abstract class ThreadedRobot : AbstractRobot
    {
        protected abstract void init();
        protected OperationMode AutonomousThread;
        protected OperationMode DisabledThread;
        protected OperationMode TeleopThread;
        protected OperationMode TestThread;
        protected abstract void SetThreads();
        private bool isStarted = false;

        public bool IsAutonomous
        {
            get
            {
                return driverStation.Autonomous;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return driverStation.Enabled;
            }
        }

        public bool IsTeleop
        {
            get
            {
                return driverStation.OperatorControl;
            }
        }

        public bool IsTest
        {
            get
            {
                return driverStation.Test;
            }
        }

        public override void InitNetworkTables()
        {
            if (isStarted == false)
            {
                isStarted = true;
                NetworkTable.SetNetworkIdentity("Robot");
                NetworkTable.SetPersistentFilename("/home/lvuser/networktables.ini");
                NetworkTable.SetServerMode();
                NetworkTable.GetTable("");
                NetworkTable.GetTable("Droo");
                NetworkTable droo = NetworkTable.GetTable("Droo");
            }
        }

        internal override void ProgramLoop()
        {
            SmartConsole.PrintInfo("Starting threadedrobot");
            HALDriverStation.HAL_ObserveUserProgramStarting();
            Report(ResourceType.kResourceType_Framework, Instances.kFramework_Sample);
            SmartConsole.PrintInfo("Initialising");

            try
            {
                init();
            }

            catch (Exception exeption)
            {
                SmartConsole.PrintError("Error on init: " + exeption.Message, exeption.StackTrace);
            }

            SetGenericThreads();
            SetThreads();

            while (true)
            {
                if (IsEnabled == false)
                {
                    SmartConsole.PrintInfo("Starting disabled thread");
                    driverStation.InDisabled(true);
                    DisabledThread.Start();

                    while (IsEnabled == false)
                    {
                        CheckBrownOut();
                        Snooze(10);
                    }

                    SmartConsole.PrintInfo("Stopping disabled thread");
                    DisabledThread.Stop();
                    driverStation.InDisabled(false);
                }

                else if (IsAutonomous == true)
                {
                    SmartConsole.PrintInfo("Starting autonomous thread");
                    driverStation.InAutonomous(true);
                    AutonomousThread.Start();

                    while (IsAutonomous == true && IsEnabled == true)
                    {
                        CheckBrownOut();
                        Snooze(10);
                    }

                    SmartConsole.PrintInfo("Stopping autonomous thhread");
                    AutonomousThread.Stop();
                    driverStation.InAutonomous(false);
                }

                else if (IsTeleop)
                {
                    SmartConsole.PrintInfo("Starting teleop thread");
                    driverStation.InOperatorControl(true);
                    TeleopThread.Start();

                    while (IsTeleop == true && IsEnabled == true)
                    {
                        CheckBrownOut();
                        Snooze(10);
                    }

                    SmartConsole.PrintInfo("Stopping teleop thread");
                    TeleopThread.Stop();
                    driverStation.InOperatorControl(false);
                }

                else if (IsTest == true)
                {
                    SmartConsole.PrintInfo("Starting test thread");
                    driverStation.InTest(true);
                    TestThread.Start();

                    while (IsTest == true && IsEnabled == true)
                    {
                        CheckBrownOut();
                        Snooze(10);
                    }

                    SmartConsole.PrintInfo("Stopping test thread");
                    TestThread.Stop();
                    driverStation.InTest(false);
                }
            }
        }

        protected void Snooze(int milliSeconds)
        {
            Thread.Sleep(milliSeconds);
        }

        private void CheckBrownOut()
        {
            if (driverStation.BrownedOut == true)
            {
                SmartConsole.PrintWarning("Rio is browned out, either replace the battery of check your wiring");
            }
        }
        
        private void SetGenericThreads()
        {
            AutonomousThread = new GenericOperationMode();
            DisabledThread = new GenericOperationMode();
            TeleopThread = new GenericOperationMode();
            TestThread = new GenericOperationMode();
        }
    }
}