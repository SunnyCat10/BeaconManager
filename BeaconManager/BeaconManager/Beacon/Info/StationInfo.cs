using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconManager.Beacon.Info
{
    internal struct StationInfo
    {
        internal string ID { get; private set; }
        internal string Name { get; private set; }
        internal string Permission { get; private set; }

        internal StationInfo(string id, string name, string permission)
        {
            ID = CheckID(id) ? id : Constants.Settings.DEFAULT_ID;
            Name = name;
            Permission = permission;
        }

        internal bool Equals(StationInfo other)
        {
            return ((ID.Equals(other.ID)) && (Name.Equals(other.Name)) && (Permission.Equals(other.Permission)));
        }

        internal new string ToString()
        {
            return ID + '\n' + Name + '\n' + Permission;
        }

        private static bool CheckID(string id)
        {
            return ( (id.Length == Constants.Settings.ID_LENGTH) && 
                (id.Substring(Constants.Settings.ID_SUFFIX_INDEX).Equals(Constants.Settings.ID_PREFIX)) );
        }
    }
}
