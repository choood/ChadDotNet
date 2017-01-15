using ChadDotNet.Etc;
using ChadDotNet.Network;
using ChadDotNet.Var;
using System;

namespace ChadDotNet.Hardware
{
    public abstract class HardwareObject : Base
    {
        public abstract void Init();
        public abstract void Stop();
        public abstract void TestLoop();

        public string Name
        {
            get
            {
                return name;
            }

            protected set
            {
                name = value;
            }
        }

        public object Shadow
        {
            get
            {
                if (SystemVariables.UserConfig.ShadowRefrence == true)
                {
                    return shadowpointer;
                }

                else
                {
                    SmartConsole.PrintError("Shadow refrences are disabled", Environment.StackTrace);
                    return null;
                }
            }
        }

        protected abstract object shadowpointer
        {
            get;
        }

        protected void register()
        {
            CoreMapAbstract.addComponent(this);
            SmartConsole.PrintInfo("Component " + name + " sucsessfuly registered.");
        }

        private string name = "";
    }
}