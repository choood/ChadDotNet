using ChadDotNet.Var;
using ChadDotNet.Network;
using System.Threading;

namespace ChadDotNet.Etc
{
    public abstract class Base
    {
        protected void vcheck()
        {
            if (SystemVariables.UserConfig.Verbose == true)
            {
                thread = new Thread(update);
                thread.Start();
            }
        }

        internal string Verbose
        {
            set
            {
                message = value;
                Thread.Sleep(1);
            }
        }

        private string message = "_";
        private Thread thread;

        private void update()
        {
            while (true)
            {
                if (message == "_")
                {
                    Thread.Sleep(10);
                }

                else
                {
                    SmartConsole.PrintVerbose(message);
                    message = "_";
                }
            }
        }
    }
}
