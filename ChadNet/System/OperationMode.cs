using ChadDotNet.Etc;
using ChadDotNet.Network;
using System;
using System.Threading;

namespace ChadDotNet.System
{
    public abstract class OperationMode : Base
    {
        public OperationMode()
        {
            vcheck();
        }

        protected abstract void Init();
        protected abstract void End();
        protected abstract void Main();
        private Thread thread;
        private bool isStarted = false;

        internal void Start()
        {
            if (isStarted == false)
            {
                isStarted = true;
                thread = new Thread(_Init);
                thread.Start();
                Snooze(100);

                if (thread.IsAlive)
                {
                    thread.Abort();
                }

                thread = new Thread(_Main);
                thread.Start();
            }

            else
            {
                SmartConsole.PrintError("Thread already started", Environment.StackTrace);
            }
        }

        internal void Stop()
        {
            if (isStarted == true)
            {
                isStarted = false;

                if (thread.IsAlive == true)
                {
                    thread.Abort();
                }

                thread = new Thread(_End);
                thread.Start();
                Snooze(10);

                if (thread.IsAlive == true)
                {
                    thread.Abort();
                }
            }

            else
            {
                SmartConsole.PrintError("Thread has not started", Environment.StackTrace);
            }
        }
       
        private void _Init()
        {
            try
            {
                Init();
                Thread.CurrentThread.Join();
            }

            catch (ThreadAbortException)
            {
                return;
            }

            catch (Exception exeption)
            {
                SmartConsole.PrintError("Exeption on thread: " + exeption.Message, exeption.StackTrace);
            }
        }

        private void _End()
        {
            try
            {
                End();
                Thread.CurrentThread.Join();
            }

            catch (ThreadAbortException)
            {
                return;
            }

            catch (Exception exeption)
            {
                SmartConsole.PrintError("Exeption on thread: " + exeption.Message, exeption.StackTrace);
            }
        }

        private void _Main()
        {
            try
            {
                Main();
                Thread.CurrentThread.Join();
            }

            catch (ThreadAbortException)
            {
                return;
            }

            catch (Exception exeption)
            {
                SmartConsole.PrintError("Exeption on thread: " + exeption.Message, exeption.StackTrace);
            }
        }

        protected void Snooze(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}