using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeaconManager.Constants;
using BeaconManager.Cataloging;
using BeaconManager.Beacon.Info;

namespace BeaconManager.Beacon
{
    internal static class BeaconFactory // Not Kosher :P
    {
        private static int Instances = 0;

        public static BeaconStation Create(StationInfo statInfo, CatalogStation catStat)
        {
            if (Instances < Settings.STATIONS_AMOUNT) 
            {
                ++Instances;
                BeaconStation tempStation = new BeaconStation(statInfo, catStat);
                tempStation.Index = Instances.ToString();
                return tempStation;
            }
            else
            {
                return null;
            }
        }
    }
}
