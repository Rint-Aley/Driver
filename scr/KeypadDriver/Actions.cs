using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using KeypadDriver.DataTypes;

namespace KeypadDriver
{
    internal class Actions
    {
        enum action : byte
        {
            open = 0,
            type = 1,
            pressCombination = 2
        }
        public void open(string path)
        {
            Process process = new Process();
            process.StartInfo.FileName = path;
            try { process.Start(); }
            catch { }
        }
        public void type(string text)
        {
            SendKeys.SendWait(text);
        }
        public void pressCombination(Combination combination)
        {
            int len = combination.getLength();
            for (int i = 0; i < len; i++)
            {
                var nodeOfCombination = combination.getCombination();
                SendKeys.SendWait(nodeOfCombination.keys);
                Thread.Sleep(nodeOfCombination.delay);
            }
        }
    }
}
