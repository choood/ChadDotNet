using ChadDotNet.Network;

namespace ChadDotNet.Hardware
{
    public class Encoder : HardwareObject
    {
        private WPILib.Encoder shadow;

        public Encoder(short channela, short channelb, string name)
        {
            vcheck();
            shadow = new WPILib.Encoder(channela, channelb);
            shadow.Reset();
            Name = name;
            Reverse = false;
            SmartConsole.PrintInfo("Encoder '" + Name + "' sucsessfuly created");
            register();
        }

        public int Value
        {
            get
            {
                return shadow.GetRaw();
            }
        }

        public void Reset()
        {
            shadow.Reset();
        }

        public bool Reverse
        {
            set
            {
                reverse = value;
            }
            get
            {
                return reverse;
            }
        }

        protected override object shadowpointer
        {
            get
            {
                return shadow;
            }
        }

        public override void Init() {}
        public override void Stop() {}

        public override void TestLoop()
        {
        }

        private bool reverse;
    }
}
