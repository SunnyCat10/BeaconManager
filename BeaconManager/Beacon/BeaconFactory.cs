using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BeaconManager.Constants;
using BeaconManager.Cataloging;
using BeaconManager.Beacon.Info;
using BeaconManager.Beacon.Technical;
using BeaconManager.Beacon.Hitches;

namespace BeaconManager.Beacon
{
    internal static class BeaconFactory // Not Kosher :P
    {
        private static int Instances = 0;

        public static BeaconStation Create(StationInfo statInfo, CatalogStation catStat, TechInfo techInfo) //Hitch hitches)
        {
            if (Instances < Settings.STATIONS_AMOUNT) 
            {
                ++Instances;
                BeaconStation tempStation = new BeaconStation(statInfo, catStat, techInfo);//, hitches);
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
