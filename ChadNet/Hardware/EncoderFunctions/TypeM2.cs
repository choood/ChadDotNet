namespace ChadDotNet.Hardware.EncoderFunctions
{
    internal class TypeM2 : Function
    {
        private bool finished;
        private long target;
        private long limit;
        private long slope;

        internal TypeM2(long exacttarget, long tolerence, long slopelimit)
        {
            finished = false;
            target = exacttarget;
            limit = tolerence;
            slope = slopelimit;
        }

        internal override bool inbounds
        {
            get
            {
                return finished;
            }
        }

        internal override double process(long value)
        {
            if (value >= target - limit && value <= target + limit)
            {
                finished = true;
            }

            else
            {
                finished = false;
            }

            if (value >= target + slope)
            {
                return -1;
            }

            else if (value <= target - slope)
            {
                return 1;
            }

            else
            {
                return ((-1.00 / slope) * value) + (target / slope);
            }
        }
    }
}