using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeaconManager.Cataloging;
using BeaconManager.Beacon.Info;
using BeaconManager.Beacon.Technical;
using BeaconManager.Beacon.Hitches;

namespace BeaconManager.Beacon
{
    internal class BeaconStation
    {
        public string Index { get; set; }
        public CatalogStation Catalog { get; set; }
        public StationInfo Info { get; private set; }
        internal TechInfo TechInformation { get; private set; }
        internal Hitch Hitches { get; set; }

        public BeaconStation(StationInfo statInfo, CatalogStation catStat, TechInfo techInfo)//, Hitch hitches)
        {
            Info = statInfo;
            Catalog = catStat;
            TechInformation = techInfo;
            //Hitches = hitches;
        }
        

        public new string ToString()
        {
            return Index + '\t' + Catalog.ToString() + '\t' + TechInformation.ToString();
        }

    }
}
