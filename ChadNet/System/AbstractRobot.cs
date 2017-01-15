using ChadDotNet.Etc;
using NetworkTables;
using WPILib;

namespace ChadDotNet.System
{
    public abstract class AbstractRobot : Base
    {
        public static DriverStation driverStation;
        internal abstract void ProgramLoop();
        private bool isStarted = false;

        public virtual void InitNetworkTables()
        {
            if (isStarted == false)
            {
                isStarted = true;
                NetworkTable.SetNetworkIdentity("Robot");
                NetworkTable.SetPersistentFilename("/home/lvuser/networktables.ini");
                NetworkTable.SetServerMode();
                NetworkTable.Initialize();
                NetworkTable.GetTable("");
                NetworkTable.GetTable("LiveWindow").GetSubTable("~STATUS~").PutBoolean("LW Enabled", false);
            }
        }

        internal void PreInit(DriverStation driverStationInstatnce)
        {
            driverStation = driverStationInstatnce;
        }
    }
}