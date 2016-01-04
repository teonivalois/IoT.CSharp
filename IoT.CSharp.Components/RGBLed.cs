using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace IoT.CSharp.Components
{
    public class RGBLed
    {

        private GpioPin _redPin;
        private GpioPin _greenPin;
        private GpioPin _bluePin;

        private GpioPinValue _redPinValue;
        private GpioPinValue _greenPinValue;
        private GpioPinValue _bluePinValue;

        public RGBLed(int redPinNumber, int greenPinNumber, int bluePinNumber)
        {
            var gpio = GpioController.GetDefault();

            _redPin = gpio.OpenPin(redPinNumber);
            _redPin.SetDriveMode(GpioPinDriveMode.Output);

            _greenPin = gpio.OpenPin(greenPinNumber);
            _greenPin.SetDriveMode(GpioPinDriveMode.Output);

            _bluePin = gpio.OpenPin(bluePinNumber);
            _bluePin.SetDriveMode(GpioPinDriveMode.Output);

            _redPinValue = GpioPinValue.Low;
            _redPin.Write(_redPinValue);

            _greenPinValue = GpioPinValue.Low;
            _greenPin.Write(_greenPinValue);

            _bluePinValue = GpioPinValue.Low;
            _bluePin.Write(_bluePinValue);
        }

        public RGBLed(GpioPin redPin, GpioPin greenPin, GpioPin bluePin)
        {
            _redPin = redPin;
            _redPin.SetDriveMode(GpioPinDriveMode.Output);

            _greenPin = greenPin;
            _greenPin.SetDriveMode(GpioPinDriveMode.Output);

            _bluePin = bluePin;
            _bluePin.SetDriveMode(GpioPinDriveMode.Output);

            _redPinValue = GpioPinValue.Low;
            _redPin.Write(_redPinValue);

            _greenPinValue = GpioPinValue.Low;
            _greenPin.Write(_greenPinValue);

            _bluePinValue = GpioPinValue.Low;
            _bluePin.Write(_bluePinValue);
        }

        public void TurnRed()
        {
            _redPinValue = GpioPinValue.High;
            _redPin.Write(_redPinValue);

            _greenPinValue = GpioPinValue.Low;
            _greenPin.Write(_greenPinValue);

            _bluePinValue = GpioPinValue.Low;
            _bluePin.Write(_bluePinValue);
        }

        public void TurnGreen()
        {
            _redPinValue = GpioPinValue.Low;
            _redPin.Write(_redPinValue);

            _greenPinValue = GpioPinValue.High;
            _greenPin.Write(_greenPinValue);

            _bluePinValue = GpioPinValue.Low;
            _bluePin.Write(_bluePinValue);
        }

        public void TurnBlue()
        {
            _redPinValue = GpioPinValue.Low;
            _redPin.Write(_redPinValue);

            _greenPinValue = GpioPinValue.Low;
            _greenPin.Write(_greenPinValue);

            _bluePinValue = GpioPinValue.High;
            _bluePin.Write(_bluePinValue);
        }

        public void TurnWhite()
        {
            _redPinValue = GpioPinValue.High;
            _redPin.Write(_redPinValue);

            _greenPinValue = GpioPinValue.High;
            _greenPin.Write(_greenPinValue);

            _bluePinValue = GpioPinValue.High;
            _bluePin.Write(_bluePinValue);
        }

        public void TurnOff()
        {
            _redPinValue = GpioPinValue.Low;
            _redPin.Write(_redPinValue);

            _greenPinValue = GpioPinValue.Low;
            _greenPin.Write(_greenPinValue);

            _bluePinValue = GpioPinValue.Low;
            _bluePin.Write(_bluePinValue);
        }

        public void Toggle()
        {
            if (IsOff())
                TurnRed();
            else if (IsRed())
                TurnGreen();
            else if (IsGreen())
                TurnBlue();
            else if (IsBlue())
                TurnWhite();
            else if (IsWhite())
                TurnOff();
        }

        private bool IsRed()
        {
            return _redPinValue == GpioPinValue.High && _greenPinValue == GpioPinValue.Low && _bluePinValue == GpioPinValue.Low;
        }

        private bool IsGreen()
        {
            return _redPinValue == GpioPinValue.Low && _greenPinValue == GpioPinValue.High && _bluePinValue == GpioPinValue.Low;
        }

        private bool IsBlue()
        {
            return _redPinValue == GpioPinValue.Low && _greenPinValue == GpioPinValue.Low && _bluePinValue == GpioPinValue.High;
        }

        private bool IsWhite()
        {
            return _redPinValue == GpioPinValue.High && _greenPinValue == GpioPinValue.High && _bluePinValue == GpioPinValue.High;
        }

        private bool IsOff()
        {
            return _redPinValue == GpioPinValue.Low && _greenPinValue == GpioPinValue.Low && _bluePinValue == GpioPinValue.Low;
        }

    }
}
