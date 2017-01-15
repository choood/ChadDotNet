namespace ChadDotNet.Hardware.EncoderFunctions
{
    internal class TypeH1 : Function
    {
        private long tolerencec;
        private long tolerencecc;

        internal TypeH1(long exacttarget, long tolerence)
        {
            tolerencec = exacttarget + tolerence;
            tolerencecc = exacttarget - tolerence;
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
            if (value > tolerencec)
            {
                return -1;
            }

            else if (value < tolerencecc)
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
