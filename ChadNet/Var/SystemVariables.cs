using ChadDotNet.Etc;

namespace ChadDotNet.Var
{
    public static class SystemVariables
    {
        public static Config UserConfig
        {
            get
            {
                return con;
            }

            internal set
            {
                con = value;
            }
        }

        private static Config con;
    }
}
