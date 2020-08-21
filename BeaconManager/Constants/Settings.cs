using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconManager.Constants
{
    internal static class Settings
    {
        internal static readonly int STATIONS_AMOUNT = 36;

        internal static readonly string TRAINING_XML_PATH = "Not implemented";
        internal static readonly string STATION_XML_PATH = "Not implemented";

        internal const string DEFAULT_ID = "0000000";
        internal const string ID_PREFIX = "105";
        internal const int ID_LENGTH = 7;
        internal const int ID_SUFFIX_INDEX = 3;
    }
}
