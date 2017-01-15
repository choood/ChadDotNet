using ChadDotNet.Network;
using System;

namespace ChadDotNet.Hardware
{
    class Camera : HardwareObject
    {
        public Camera(string name)
        {
            vcheck();
            Name = name;
            shadow = WPILib.CameraServer.Instance;
            started = false;
            SmartConsole.PrintInfo("Camera '" + Name + "' sucsessfuly created");
            register();
        }

        protected override object shadowpointer
        {
            get
            {
                return shadow;
            }
        }

        public override void Init()
        {
            if (started == true)
            {
                started = false;
                shadow.StartAutomaticCapture();
            }
        }

        public override void Stop() { }

        public override void TestLoop() { }

        private WPILib.CameraServer shadow;
        private bool started;
    }
}
