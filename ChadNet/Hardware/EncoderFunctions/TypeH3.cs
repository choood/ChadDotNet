namespace ChadDotNet.Hardware.EncoderFunctions
{
    internal class TypeH3 : Function
    {
        private long target;
        private long lowerc;
        private long lowercc;
        private long upperc;
        private long uppercc;

        internal TypeH3(long exacttarget, long uppertolerence, long lowertolerence)
        {
            target = exacttarget;
            lowerc = exacttarget + lowertolerence;
            lowercc = exacttarget - lowertolerence;
            upperc = exacttarget + uppertolerence;
            uppercc = exacttarget - uppertolerence;
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
            if (value < lowerc && value > lowercc)
            {
                return 0;
            }

            else if (value > upperc)
            {
                return -1;
            }

            else if (value < uppercc)
            {
                return 1;
            }

            else if (value >= lowerc && value <= upperc)
            {
                return ((1.00 / (upperc - lowerc)) * (lowerc - value));
            }

            else if (value <= lowercc && value >= uppercc)
            {
                return ((1.00 / (uppercc - lowercc)) * (value - lowercc));
            }

            else
            {
                return 0;
            }
        }

    }
}