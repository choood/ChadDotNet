using ChadDotNet.Etc;

namespace ChadDotNet.Hardware.EncoderFunctions
{
    internal abstract class Function : Base
    {
        internal abstract bool inbounds
        {
            get;
        }

        internal abstract double process(long value);
    }
}