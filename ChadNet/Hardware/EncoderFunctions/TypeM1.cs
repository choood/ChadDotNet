namespace ChadDotNet.Hardware.EncoderFunctions
{
    internal class TypeM1 : Function
    {
        private bool finished;
        private long target;
        private long limit;

        internal TypeM1(long exacttarget, long tolerence)
        {
            finished = false;
            target = exacttarget;
            limit = tolerence;
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
            if (value >= target + limit)
            {
                finished = false;
                return -1;
            }

            else if (value <= target - limit)
            {
                finished = false;
                return 1;
            }

            else
            {
                finished = true;
                return 0;
            }
        }
    }
}
