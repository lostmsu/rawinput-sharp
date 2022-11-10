using System;
using System.Linq;
using System.Windows.Forms;
using Linearstar.Windows.RawInput;

namespace RawInput.Sharp.SimpleExample
{
    class RawInputConsole
    {
        static void Main()
        {
            // Get the devices that can be handled with Raw Input.
            var devices = RawInputDevice.GetDevices();

            var mice = devices.OfType<RawInputMouse>();

            // List them up.
            foreach (var device in mice)
                Console.WriteLine($"{device.DeviceType} {device.VendorId:X4}:{device.ProductId:X4} {device.ProductName}, {device.ManufacturerName}");

            // To begin catching inputs, first make a window that listens WM_INPUT.
            var window = new RawInputReceiverWindow();

            window.Input += (sender, e) =>
            {
                // Catch your input here!
                var data = e.Data;

                Console.WriteLine(data);
            };

            try
            {
                // Register the HidUsageAndPage to watch any device.
                RawInputDevice.RegisterDevice(HidUsageAndPage.Mouse, RawInputDeviceFlags.NoLegacy | RawInputDeviceFlags.InputSink, window.Handle);

                Application.Run();
            }
            finally
            {
                RawInputDevice.UnregisterDevice(HidUsageAndPage.Mouse);
            }
        }
    }
}
