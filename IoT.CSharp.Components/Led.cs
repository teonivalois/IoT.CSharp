using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace IoT.CSharp.Components
{
    public class Led
    {

        private GpioPin _pin;
        private GpioPinValue _pinValue;

        public Led(int pinNumber)
        {
            var gpio = GpioController.GetDefault();
            _pin = gpio.OpenPin(pinNumber);
            _pin.SetDriveMode(GpioPinDriveMode.Output);

            _pinValue = GpioPinValue.Low;
            _pin.Write(_pinValue);
        }

        public Led(GpioPin pin)
        {
            _pin = pin;

            _pin.SetDriveMode(GpioPinDriveMode.Output);

            _pinValue = GpioPinValue.Low;
            _pin.Write(_pinValue);
        }

        public void TurnOn()
        {
            _pinValue = GpioPinValue.High;
            _pin.Write(_pinValue);
        }

        public void TurnOff()
        {
            _pinValue = GpioPinValue.Low;
            _pin.Write(_pinValue);
        }

        public void Toggle()
        {
            _pinValue = _pinValue == GpioPinValue.High ? GpioPinValue.Low : GpioPinValue.High;
            _pin.Write(_pinValue);
        }

    }
}
