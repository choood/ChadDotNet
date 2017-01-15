using System;

namespace ChadDotNet.Etc
{
    public abstract class Config
    {
        public abstract bool Verbose
        {
            get;
        }

        public abstract bool ShadowRefrence
        {
            get;
        }

        public abstract bool Log
        {
            get;
        }

        public virtual string Path
        {
            get
            {
                return "/tmp/";
            }
        }

        public abstract Type RobotClass
        {
            get;
        }

        public abstract string Version
        {
            get;
        }

        public abstract string[] Splash
        {
            get;
        }
    }
}
