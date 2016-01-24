using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace IoT.CSharp.Components.StepperMotors
{
    public class SM_28BYJ_48 : IStepperMotor
    {
        public GpioPin[] Pins { get; set; }

        private readonly int[][] ClockwiseSequences = new[] {
                                new [] {1, 0, 0, 0},
                                new [] {1, 1, 0, 0},
                                new [] {0, 1, 0, 0},
                                new [] {0, 1, 1, 0},
                                new [] {0, 0, 1, 0},
                                new [] {0, 0, 1, 1},
                                new [] {0, 0, 0, 1},
                                new [] {1, 0, 0, 1}};

        private readonly int[][] AntiClockwiseSequences = new[] {
                                 new[] {1, 0, 0, 0},
                                 new[] {1, 0, 0, 1},
                                 new[] {0, 0, 0, 1},
                                 new[] {0, 0, 1, 1},
                                 new[] {0, 0, 1, 0},
                                 new[] {0, 1, 1, 0},
                                 new[] {0, 1, 0, 0},
                                 new[] {1, 1, 0, 0}};

        public SM_28BYJ_48(params GpioPin[] pins)
        {
            Pins = pins;
            Reset();
        }

        public SM_28BYJ_48(params int[] pinNumbers)
        {
            var gpio = GpioController.GetDefault();

            IList<GpioPin> pins = new List<GpioPin>();
            foreach (var pinNumber in pinNumbers)
                pins.Add(gpio.OpenPin(pinNumber));

            Pins = pins.ToArray();
            Reset();
        }

        public void Reset()
        {
            foreach (var pin in Pins)
            {
                pin.SetDriveMode(GpioPinDriveMode.Output);
                pin.Write(GpioPinValue.Low);
            }
        }

        public void Backward(double amount, MovementUnit unit)
        {
            Move(amount > 0 ? 0 : amount, unit, AntiClockwiseSequences);
        }

        public void Forward(double amount, MovementUnit unit)
        {
            Move(amount < 0 ? 0 : amount, unit, ClockwiseSequences);
        }

        private void Move(double amount, MovementUnit unit, int[][] sequences)
        {
            //1revolution = 512steps
            int steps = (int)Math.Floor(amount * (unit == MovementUnit.Step ? 1 : 512));
            for (int step = 0; step < steps; step++)
            {
                foreach (var sequence in sequences)
                {
                    for (int i = 0; i < Pins.Length; i++)
                    {
                        Pins[i].Write(sequence[i] == 0 ? GpioPinValue.Low : GpioPinValue.High);
                    }
                    Task.Delay(1).Wait(); //Sleep(0.001)
                }
            }
        }
    }
}
