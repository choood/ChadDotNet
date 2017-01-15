using System.Collections.Generic;

namespace ChadDotNet.Hardware
{
    public abstract class CoreMapAbstract
    {
        public abstract void init();
        private static List<HardwareObject> registry = new List<HardwareObject>();

        public static void Init()
        {
            foreach (HardwareObject component in registry)
            {
                component.Init();
            }
        }

        public static void Stop()
        {
            foreach (HardwareObject component in registry)
            {
                component.Stop();
            }
        }

        public static void TestLoop()
        {
            foreach (HardwareObject component in registry)
            {
                component.TestLoop();
            }
        }

        internal static void addComponent(HardwareObject component)
        {
            registry.Add(component);
        }
    }
}