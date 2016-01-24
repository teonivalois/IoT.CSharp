using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace IoT.CSharp.Components.StepperMotors
{
    public enum MovementUnit
    {
        Revolution,
        Step
    }

    public interface IStepperMotor
    {

        GpioPin[] Pins { get; set; }

        void Reset();

        void Forward(double amount, MovementUnit unit);
        void Backward(double amount, MovementUnit unit);
    }
}
