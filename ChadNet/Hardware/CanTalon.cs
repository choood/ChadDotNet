using ChadDotNet.Network;

namespace ChadDotNet.Hardware
{
    public class CanTalon : MotorController
    {
        public CanTalon(int id, string name)
        {
            vcheck();
            shadow = new WPILib.CANTalon(id);
            Name = name;
            setup();
            SmartConsole.PrintInfo("CanTalon '" + Name + "' sucsessfuly created");
            register();
        }

        protected override double protectedacsesspointer
        {
            get
            {
                return shadow.Get();
            }

            set
            {
                shadow.Set(value);
            }
        }

        public override bool Saftey
        {
            get
            {
                return shadow.SafetyEnabled;
            }

            set
            {
                shadow.SafetyEnabled = value;
            }
        }

        protected override object shadowpointer
        {
            get
            {
                return shadow;
            }
        }

        private WPILib.CANTalon shadow;
    }
}