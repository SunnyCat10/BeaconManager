using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BeaconManager.Constants;

namespace BeaconManager.Beacon.Info
{
    /// <summary>
    /// StationInfo
    /// </summary>
    internal struct StationInfo
    {
        internal string ID { get; private set; }
        internal string Name { get; private set; }
        internal string Permission { get; private set; }

        internal StationInfo(string id, string name, string permission)
        {
            ID = CheckID(id) ? id : Settings.DEFAULT_ID;
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
            bool length = (id.Length == Settings.ID_LENGTH);
            bool prefix = id.Substring(0, Settings.ID_SUFFIX_INDEX).Equals(Settings.ID_PREFIX);

            return (length && prefix);
        }
    }
}
