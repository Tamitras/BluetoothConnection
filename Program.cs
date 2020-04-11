using BluetoothTest.Bluetooth;
using System;

namespace BluetoothTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var controller = new BluetoothController();
            controller.Start();

            Console.ReadKey();
        }
    }
}
