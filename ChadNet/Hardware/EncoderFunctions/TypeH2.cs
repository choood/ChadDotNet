namespace ChadDotNet.Hardware.EncoderFunctions
{
    internal class TypeH2 : Function
    {
        private long target;
        private long limit;

        internal TypeH2(long exacttarget, long tolerence)
        {
            target = exacttarget;
            limit = tolerence;
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
            if (value >= target + limit)
            {
                return -1;
            }

            else if (value <= target - limit)
            {
                return 1;
            }

            else
            {
                return ((-1.00 / limit) * value) + (target / limit);
            }
        }
    }
}