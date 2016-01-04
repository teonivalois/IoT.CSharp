using IoT.CSharp.AdafruitIO;
using IoT.CSharp.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.CSharp.Tests
{
    public class MyIoTClient : AdafruitIOClient
    {

        private readonly Led _redLed = new Led(4);
        private readonly RGBLed _rgbLed = new RGBLed(17, 27, 22);

        public MyIoTClient()
            : base("teonivalois", "YOUR_KEY_HERE")
        {

            Feed["iotexperience"] = message => {

                if ("ON".Equals(message))
                    _redLed.TurnOn();
                else if ("OFF".Equals(message))
                    _redLed.TurnOff();
                else if ("RED".Equals(message))
                    _rgbLed.TurnRed();
                else if ("GREEN".Equals(message))
                    _rgbLed.TurnGreen();
                else if ("BLUE".Equals(message))
                    _rgbLed.TurnBlue();
                else if ("WHITE".Equals(message))
                    _rgbLed.TurnWhite();
                else if ("BLACK".Equals(message))
                    _rgbLed.TurnOff();

            };
        }

    }
}
