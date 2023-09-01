using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KeypadDriver.DataTypes;

namespace KeypadDriver
{
    internal class Parser
    {
        private byte oldButtonsState, currentButtonsState;
        Actions actions = new Actions();
        public void start()
        {
            Console.WriteLine("parser was started");
            while (true)
            {
                checkForChanges();
            }
        }
        private void parse()
        {
            //Parser for only 8-buttons keypad
            byte changing = (byte)(currentButtonsState & ~oldButtonsState);
            Console.WriteLine($"I've found changing {changing}"); ;
            for (int i = 0; i < 8; i++)
            {
                if (Convert.ToBoolean(changing & (byte)Math.Pow(2, i)))
                {
                    execute(i+1);
                }
            }

            //Theuniversal parser (for any-buttons keypad)

            //for (int i = 0; i < Container.keysNuber; i++)
            //{
            //    if (Convert.ToBoolean(Container.buttonsState[i / 8] & (byte)Math.Pow(2, i % 8)))
            //    {
            //        execute(i + 1);
            //    }
            //}
        }

        private void checkForChanges()
        {
            try 
            {
                currentButtonsState = Container.buttonsStateQueue.Dequeue();
                if(currentButtonsState != oldButtonsState)
                {
                    parse();
                    oldButtonsState = currentButtonsState;
                }
            }
            catch
            {
                //In this case buttonsStateQueue don't have any elements, so we have not work for a while.
            }
        }
        
        public void execute(int key)
        {
            string statement = FileReader.readLine(key);
            string[] partsOfStatement = statement.Split(' ');
            string arg = String.Empty;
            for(int i = 2; i < partsOfStatement.Length - 1; i++)
            {
                arg += partsOfStatement[i] + " ";
            }
            arg += partsOfStatement[partsOfStatement.Length - 1];

            //TODO: Threads

            switch (partsOfStatement[1])
            {
                case "open":
                    actions.open(arg);
                    break;

                case "type":
                    actions.type(arg);
                    break;

                case "pressCombination":
                    Combination combination = new Combination(arg);
                    actions.pressCombination(combination);
                    break;
            }
            
        }
    }

    static class Container
    {
        static public int keysNuber = 8;
        static public Queue<byte> buttonsStateQueue = new Queue<byte>();
    }
}
