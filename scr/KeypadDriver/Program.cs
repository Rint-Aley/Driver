using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HidSharp.Experimental;
using HidSharp.Reports;
using HidSharp.Reports.Encodings;
using HidSharp.Utility;
using HidSharp;
using System.Threading;
using KeypadDriver.DataTypes;

namespace KeypadDriver
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //testCombinationDataStructure();

            Parser parser = new Parser();
            //Thread.Sleep(1000);
            //parser.execute(3);
            //return;

            Device myDevice = null;
            var list = DeviceList.Local;
            var hidDeviceList = list.GetHidDevices().ToArray();
            Console.WriteLine("All device list:");
            foreach (var hidDevice in hidDeviceList)
            {
                Console.WriteLine(hidDevice.VendorID + " " + hidDevice.ProductID);
                if(hidDevice.VendorID == 1155 && hidDevice.ProductID == 22315)
                {
                    Console.WriteLine("It's my device");
                    myDevice = hidDevice;
                }
            }

            
            Thread thread = new Thread(new ThreadStart(parser.start));
            thread.Start();

            if (myDevice != null)
            {
                var deviseStream = myDevice.Open();
                byte[] buffer = new byte[2];

                while (true)
                {
                    deviseStream.Read(buffer, 0, 2);
                    Container.buttonsStateQueue.Enqueue(buffer[1]);
                }
                 
            }
            MessageBox.Show("Device wasn't found");
            //VID: 1'155 PID: 22'315
        }

        static void testCombinationDataStructure()
        {
            Console.WriteLine("This is test the Combination data structure");
            string firstCombination = "a|d|ctrl";
            string secondCombination = "";
            string thirdCombination = "a|d|ctrl 10 Alt|f10";
            Combination[] comb = { new Combination(firstCombination), new Combination(secondCombination), new Combination(thirdCombination) };
            for(int j = 0; j < 3; j++)
            {
                Console.WriteLine($"Combination {j + 1}:");
                int len = comb[j].getLength();
                for (int i = 0; i < len; i++)
                {
                    NodeOfCombination currentNode = comb[j].getCombination();
                    Console.WriteLine($"Node {i} have keys: {currentNode.keys} and delay: {currentNode.delay}");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
