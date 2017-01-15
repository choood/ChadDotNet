namespace ChadDotNet.Hardware.EncoderFunctions
{
    internal class TypeH0 : Function
    {
        private long target;

        internal TypeH0(long exacttarget)
        {
            target = exacttarget;
        }

        internal override bool inbounds
        {
            get
            {
                return false;
            }
        }

        internal override double process(long value)
        {
            if (value < target)
            {
                return 1;
            }

            else if (value > target)
            {
                return -1;
            }

            else
            {
                return 0;
            }
        }
    }
}
