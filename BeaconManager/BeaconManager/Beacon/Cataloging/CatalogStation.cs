using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconManager.Cataloging
{
    internal class CatalogStation
    {
        public CatalogEntity ComputerCatalog { get; set; }
        public CatalogEntity ScreenCatalog { get; set; }

        //Depreceted
        private const string COMPUTER_CATALOG = ":מחשב";
        private const string SCREEN_CATALOG = ":מסך";
        private const string CATALOG_ID = ":מקט";
        private const string CATALOG_NAME = ":שם פריט";

        public CatalogStation(CatalogEntity compCat, CatalogEntity screenCat)
        {
            ComputerCatalog = compCat;
            ScreenCatalog = screenCat;
        }

        public bool Equals(CatalogStation other)
        {
            return ((ComputerCatalog.Equals(other.ComputerCatalog))
                && (ScreenCatalog.Equals(other.ScreenCatalog)));
        }

        public new string ToString()
        {
            return ComputerCatalog.ToString() + '\n' + ScreenCatalog.ToString();
        }

        //Depreceted
        internal string PrintComputerCatalog()
        {
            return COMPUTER_CATALOG + '\n' + ComputerCatalog.CatalogID + '\t' + "       " + CATALOG_ID +
                '\n' + ComputerCatalog.CatalogName + '\t' + CATALOG_NAME;
        }

        //Depreceted
        internal string PrintScreenCatalog()
        {
            return SCREEN_CATALOG + '\n' + ScreenCatalog.CatalogID + '\t' + "       " + CATALOG_ID +
                '\n' + ScreenCatalog.CatalogName + '\t' + CATALOG_NAME;
        }
    }
}
