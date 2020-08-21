using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeaconManager.Console
{
    internal class Console
    {
        private List<List<Control>> ctrlMatrix;

        internal Control this[int x, int y]
        {
            get
            {
                return new Control();
            }
        }

        internal Console(int x, int y)
        {
            for(int i = 0; i < x; i++)
            {
                var tempList = new List<Control> { };
                for (int j = 0; j < y; j++)
                {
                    tempList.Add(null);
                }
                ctrlMatrix.Add(tempList);
            }
        }

        internal void Update(object sender, IPrintable printable)
        {
            printable.FormatString();
        }
    
    }
}
