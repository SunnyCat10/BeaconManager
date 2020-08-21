using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconManager.Cataloging
{
    public struct CatalogEntity
    {
        public string CatalogID { get; set; }
        public string CatalogName { get; set; }

        public CatalogEntity(string catId, string catName)
        {
            CatalogID = catId;
            CatalogName = catName;
        }

        internal CatalogEntity(CatalogEntity other)
        {
            CatalogID = other.CatalogID;
            CatalogName = other.CatalogName;
        }

        public bool Equals(CatalogEntity other)
        {
            return ( (CatalogID == other.CatalogID) && (CatalogName == other.CatalogName) );
        }

        public string ToString()
        {
            return CatalogName + '\n' + CatalogID;
        }
    }
}
