using ChadDotNet.Network;

namespace ChadDotNet.System
{
    class GenericOperationMode : OperationMode
    {
        protected override void End()
        {
            printWarning();
        }

        protected override void Init()
        {
            printWarning();
        }

        protected override void Main()
        {
            printWarning();
        }

        private void printWarning()
        {
            SmartConsole.PrintWarning("Thread has not been defined, running generic thread");
        }
    }
}
