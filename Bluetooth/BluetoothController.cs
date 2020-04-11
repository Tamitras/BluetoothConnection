using HashtagChris.DotNetBlueZ;
using HashtagChris.DotNetBlueZ.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothTest.Bluetooth
{
    public class BluetoothController
    {
        public List<Adapter> Adapters { get; set; } = new List<Adapter>();

        public List<Device> Devices { get; set; } = new List<Device>();

        public Adapter MainAdapter { get; set; }
        public BluetoothController()
        {

        }

        public void Start()
        {
            // Gets the Adapters
            this.GetAdapters();

            // Gets the conncted Devices
            this.GetCurrentDevices();

            // Waits for new Device has been Conntected
            this.StartDiscovering();
        }

        private async void GetCurrentDevices()
        {
            this.Devices.AddRange(await MainAdapter.GetDevicesAsync());
        }

        private async void GetAdapters()
        {
            this.Adapters.AddRange(await BlueZManager.GetAdaptersAsync());
            this.SetMainAdapter();
        }

        private void SetMainAdapter()
        {
            if (this.Adapters.Count > 0)
            {
                MainAdapter = this.Adapters.FirstOrDefault();
            }
        }

        private void StartDiscovering()
        {
            this.MainAdapter.StartDiscoveryAsync();

            this.MainAdapter.DeviceFound += async (adapter, args) =>
            {
                if (args.IsStateChange)
                {
                    this.Devices.Add(args.Device);

                    this.HandleDevice(args.Device);
                }
            };
        }

        private void HandleDevice(Device device)
        {
            new Task(async () =>
            {
                device.Connected += Device_Connected;
                device.Disconnected += Device_Disconnected;
                device.ServicesResolved += Device_ServicesResolved;

                await device.ConnectAsync();

            }).Start();
        }

        private Task Device_ServicesResolved(Device sender, BlueZEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        private Task Device_Disconnected(Device sender, BlueZEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        private Task Device_Connected(Device sender, BlueZEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
