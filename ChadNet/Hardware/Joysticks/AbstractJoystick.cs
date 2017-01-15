using ChadDotNet.Etc;
using WPILib;

namespace ChadDotNet.Hardware.Joysticks
{
    public class JoystickBase : Base
    {
        private Joystick shadow;

        public JoystickBase(int channel)
        {
            shadow = new Joystick(channel);
        }
    }
}
