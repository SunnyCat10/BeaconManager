using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeaconManager.Beacon.Info;
using BeaconManager.Cataloging;

namespace BeaconManager.Beacon
{
    internal static class BeaconBuffer
    {
        internal static List<BeaconStation> Buffer { get; private set; }

        internal static void Load()
        {
            Buffer = BeaconSerialization.Deserialize();
        }

        internal static void UpdateComputerCatalog(int station, CatalogEntity computerCatalog)
        {
            var stationToUpdate = Buffer[station - 1];
            stationToUpdate.Catalog.ComputerCatalog = new CatalogEntity(computerCatalog);
            var updatedComputerCatalog = stationToUpdate.Catalog.ComputerCatalog;
            BeaconSerialization.SerializeUpdatedComputerCatalog(station, updatedComputerCatalog);
        }

        internal static void UpdateScreenCatalog(int station, CatalogEntity screenCatalog)
        {
            var stationToUpdate = Buffer[station - 1];
            stationToUpdate.Catalog.ScreenCatalog = new CatalogEntity(screenCatalog);
            var updatedScreenCatalog = stationToUpdate.Catalog.ScreenCatalog;
            BeaconSerialization.SerializeUpdatedScreenCatalog(station, updatedScreenCatalog);
        }
    }
}
