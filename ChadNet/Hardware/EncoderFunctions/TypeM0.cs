namespace ChadDotNet.Hardware.EncoderFunctions
{
    internal class TypeM0 : Function
    {
        private bool finished;
        private long target;
        private long limit;

        internal TypeM0(long exacttarget, long tolerence)
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
            if (value >= target - limit && value <= target + limit)
            {
                finished = true;
            }

            else
            {
                finished = false;
            }

            if(value >= target)
            {
                return -1;
            }

            else if(value <= target)
            {
                return 1;
            }

            else
            {
                return 0;
            }
        }
    }
}
