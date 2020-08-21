using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeaconManager.Cataloging;
using BeaconManager.Beacon.Info;

namespace BeaconManager.Beacon
{
    internal class BeaconStation
    {
        public string Index { get; set; }
        public CatalogStation Catalog { get; set; }
        public StationInfo Info { get; private set; }
        // TechInfoStation
        // ErrorStation

        public BeaconStation(StationInfo statInfo, CatalogStation catStat)
        {
            Info = statInfo;
            Catalog = catStat;
        }

        public new string ToString()
        {
            return Index + '\t' + Catalog.ToString() + '\t' + Info.ToString() ;
        }
    }
}
