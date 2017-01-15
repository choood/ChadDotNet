using ChadDotNet.Network;
using ChadDotNet.Hardware.EncoderFunctions;
using System;
using System.Threading;

namespace ChadDotNet.Hardware
{
    public abstract class MotorController : HardwareObject
    {

        public void StopMotor()
        {
            if (IsLocked)
            {
                internalthread.Abort();
                IsLocked = false;
            }
            Power = 0;
        }

        public void addEncoder(Encoder encoder)
        {
            shadowencoder = encoder;
            HasEncoder = true;
            SmartConsole.PrintInfo("Encoder '" + shadowencoder.Name + "' sucsesfully added to controller '" + Name +"'");
        }

        public bool HasEncoder
        {
            get
            {
                return hasencoder;
            }

            protected set
            {
                hasencoder = value;
            }
        }

        public bool IsLocked
        {
            get
            {
                return islocked;
            }

            protected set
            {
                islocked = value;
            }
        }

        public long ExactTarget
        {
            get
            {
                return exacttarget;
            }

            protected set
            {
                exacttarget = value;
            }
        }

        public long LowerTolerence
        {
            get
            {
                return lowertolerence;
            }

            protected set
            {
                lowertolerence = value;
            }
        }

        public long Position
        {
            get
            {
                if (hasencoder)
                {
                    return shadowencoder.Value;
                }

                else
                {
                    SmartConsole.PrintError("No encoder present on motor " + Name + ".", Environment.StackTrace);
                    return 0;
                }
            }
        }

        public long UpperTolerence
        {
            get
            {
                return uppertolerence;
            }

            protected set
            {
                uppertolerence = value;
            }
        }

        public double PowerMultiplyer
        {
            get
            {
                return powermultiplyer;
            }

            protected set
            {
                powermultiplyer = value;
            }
        }

        public double Power
        {
            set
            {
                if (islocked)
                {
                    SmartConsole.PrintWarning("'" + Name + "' recived a command while locked, ignoring");
                }
                else
                {
                    protectedacsesspointer = value;
                }
            }

            get
            {
                return protectedacsesspointer;
            }
        }

        public int RunTime
        {
            get
            {
                return runtime;
            }

            protected set
            {
                runtime = value;
            }
        }

        public bool RunFlag
        {
            get
            {
                return runflag;
            }

            protected set
            {
                runflag = value;
            }
        }

        public long Tolerence
        {
            get
            {
                return tolerence;
            }

            protected set
            {
                tolerence = value;
            }
        }

        public MotorController Refrence
        {
            get
            {
                return refrence;
            }

            protected set
            {
                refrence = value;
            }
        }

        public void Follow(MotorController refrence)
        {
            if (IsLocked)
            {
                SmartConsole.PrintWarning("'" + Name + "' recived a command while locked, ignoring");
            }

            else
            {
                Refrence = refrence;
                IsLocked = true;
                internalthread = new Thread(follow);
                internalthread.Start();
            }
        }

        public void HoldAt(long exactvalue, long lowertolerence, long uppertolerence, double powermultiplyer)
        {
            if (IsLocked)
            {
                SmartConsole.PrintWarning("'" + Name + "' recived a command while locked, ignoring");
            }

            else
            {
                if (HasEncoder)
                {
                    ExactTarget = exactvalue;
                    LowerTolerence = lowertolerence;
                    UpperTolerence = uppertolerence;
                    PowerMultiplyer = powermultiplyer;
                    IsLocked = true;

                    if (LowerTolerence >= UpperTolerence)
                    {
                        IsLocked = false;
                        HoldAt(ExactTarget, UpperTolerence, PowerMultiplyer);
                    }

                    else if (LowerTolerence <= 0)
                    {
                        IsLocked = false;
                        holdat(ExactTarget, UpperTolerence, PowerMultiplyer);
                    }

                    else
                    {
                        type = EncoderFunctions.Type.H3;
                        internalthread = new Thread(holdat);
                        internalthread.Start();
                    }
                }

                else
                {
                    SmartConsole.PrintError("No encoder present for move to function on '" + Name + "', skipping step", Environment.StackTrace);
                }
            }
        }

        public void HoldAt(long exactvalue, long tolerence, double powermultiplyer)
        {
            if (IsLocked)
            {
                SmartConsole.PrintWarning("'" + Name + "' recived a command while locked, ignoring");
            }

            else
            {
                if (HasEncoder)
                {
                    ExactTarget = exactvalue;
                    Tolerence = tolerence;
                    PowerMultiplyer = powermultiplyer;
                    IsLocked = true;

                    if (Tolerence <= 0)
                    {
                        IsLocked = false;
                        HoldAt(ExactTarget, PowerMultiplyer);
                    }

                    else
                    {
                        type = EncoderFunctions.Type.H2;
                        internalthread = new Thread(holdat);
                        internalthread.Start();
                    }
                }

                else
                {
                    SmartConsole.PrintError("No encoder present for move to function on '" + Name + "', skipping step", Environment.StackTrace);
                }
            }
        }

        public void HoldAt(long exactvalue, double powermultiplyer)
        {
            if (IsLocked)
            {
                SmartConsole.PrintWarning("'" + Name + "' recived a command while locked, ignoring");
            }

            else
            {
                if (HasEncoder)
                {
                    ExactTarget = exactvalue;
                    PowerMultiplyer = powermultiplyer;
                    type = EncoderFunctions.Type.H0;
                    internalthread = new Thread(holdat);
                    internalthread.Start();
                }

                else
                {
                    SmartConsole.PrintError("No encoder present for move to function on '" + Name + "', skipping step", Environment.StackTrace);
                }
            }
        }

        private void holdat(long exactvalue, long tolerence, double powermultiplyer)
        {
            if (IsLocked)
            {
                SmartConsole.PrintWarning("'" + Name + "' recived a command while locked, ignoring");
            }

            else
            {
                if (HasEncoder)
                {
                    ExactTarget = exactvalue;
                    Tolerence = tolerence;
                    PowerMultiplyer = powermultiplyer;
                    IsLocked = true;

                    if (Tolerence <= 0)
                    {
                        IsLocked = false;
                        HoldAt(ExactTarget, PowerMultiplyer);
                    }

                    else
                    {
                        type = EncoderFunctions.Type.H1;
                        internalthread = new Thread(holdat);
                        internalthread.Start();
                    }
                }

                else
                {
                    SmartConsole.PrintError("No encoder present for move to function on '" + Name + "', skipping step", Environment.StackTrace);
                }
            }
        }

        public void MoveFor(int miliseconds, double power)
        {
            if (IsLocked)
            {
                SmartConsole.PrintWarning("'" + Name + "' recived a command while locked, ignoring");
            }

            else
            {
                RunTime = miliseconds;
                PowerMultiplyer = power;
                IsLocked = true;
                internalthread = new Thread(movefor);
                internalthread.Start();
            }
        }

        public string Mode
        {
            get
            {
                return type.ToString();
            }
        }

        public void MoveWhile(bool flag, double power)
        {
            if (IsLocked)
            {
                SmartConsole.PrintWarning("'" + Name + "' recived a command while locked, ignoring");
            }

            else
            {
                RunFlag = flag;
                PowerMultiplyer = power;
                IsLocked = true;
                internalthread = new Thread(movewhile);
                internalthread.Start();
            }
        }

        public void MoveTo(long exactvalue, long lowertolerence, long uppertolerence, double powermultiplyer)
        {
            if (IsLocked)
            {
                SmartConsole.PrintWarning("'" + Name + "' recived a command while locked, ignoring");
            }

            else
            {
                if (HasEncoder)
                {
                    ExactTarget = exactvalue;
                    LowerTolerence = lowertolerence;
                    UpperTolerence = uppertolerence;
                    PowerMultiplyer = powermultiplyer;
                    IsLocked = true;
                    internalthread = new Thread(moveto);
                    internalthread.Start();
                }

                else
                {
                    SmartConsole.PrintError("No encoder present for move to function on '" + Name + "', skipping step", Environment.StackTrace);
                }
            }
        }

        public override void Init()
        {
            IsLocked = false;
        }

        public override void Stop()
        {
            StopMotor();
        }

        public override void TestLoop()
        {
            throw new NotImplementedException();
        }

        public abstract bool Saftey
        {
            set;
            get;
        }

        protected Encoder shadowencoder;
        protected Thread internalthread;

        protected abstract double protectedacsesspointer
        {
            set;
            get;
        }

        protected void setup()
        {
            ExactTarget = 0;
            LowerTolerence = 0;
            PowerMultiplyer = 0;
            Refrence = null;
            RunFlag = false;
            RunTime = 0;
            Saftey = false;
            Tolerence = 0;
            UpperTolerence = 0;
            type = EncoderFunctions.Type.NULL;
        }

        private bool hasencoder;
        private bool islocked;
        private long exacttarget;
        private long lowertolerence;
        private long uppertolerence;
        private long tolerence;
        private double powermultiplyer;
        private int runtime;
        private bool runflag;
        private MotorController refrence;
        private EncoderFunctions.Type type;
        private Function function;

        private void follow()
        {
            while (true)
            {
                protectedacsesspointer = Refrence.Power;
                Thread.Sleep(10);
            }
        }

        private void holdat()
        {
            switch (type)
            {
                case EncoderFunctions.Type.H0:
                    function = new TypeH0(ExactTarget);
                    break;

                case EncoderFunctions.Type.H1:
                    function = new TypeH1(ExactTarget, UpperTolerence);
                    break;

                case EncoderFunctions.Type.H2:
                    function = new TypeH2(ExactTarget, UpperTolerence);
                    break;

                case EncoderFunctions.Type.H3:
                    function = new TypeH3(ExactTarget, UpperTolerence, LowerTolerence);
                    break;
            }

            while (true)
            {
                protectedacsesspointer = (PowerMultiplyer * function.process(shadowencoder.Value));
                Thread.Sleep(10);
            }
        }

        private void movefor()
        {
            protectedacsesspointer = PowerMultiplyer;
            Thread.Sleep(RunTime);
            protectedacsesspointer = 0;
            IsLocked = false;
        }

        private void movewhile()
        {
            protectedacsesspointer = PowerMultiplyer;

            while (RunFlag)
            {
                Thread.Sleep(10);
            }

            protectedacsesspointer = 0;
            IsLocked = false;
        }

        private void moveto()
        {
        }
    }
}