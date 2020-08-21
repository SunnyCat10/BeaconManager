using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconManager.Console
{
    interface IBufferable
    {
        object GetObject(int index);
    }
}
