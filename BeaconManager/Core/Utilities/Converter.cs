using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

using BeaconManager.Beacon.Hitches;

/// <summary>
/// @Author Daniel Dovgun
/// @Version 5/8/2020
/// </summary>
namespace BeaconManager.Core.Utilities
{
    /// <summary>
    /// Utility class for converting different values.
    /// </summary>
    public static class Converter
    {
        private const string STATION_BTN_PREFIX = "btn_";

        /// <summary>
        /// Converts Station ID to Station button name.
        /// </summary>
        /// <param name="stationId"> Station`s ID </param>
        /// <returns> Station`s ID corresponding button name. </returns>
        public static string IdToStationBtn(int stationId)
        {
            string temp = STATION_BTN_PREFIX;
            temp += stationId;
            return temp;
        }

        public static Brush HexColorToBrush(string hexColor)
        {
            var color = (Color)ColorConverter.ConvertFromString(hexColor);
            var brush = new SolidColorBrush(color);
            return brush;
        }

        /// <summary>
        /// Extracts the ID of the button that represent a specific PC station.
        /// </summary>
        /// <param name="sender"> The event sender - PC station button. </param>
        /// <returns> The ID of the specific PC station </returns>
        internal static int ExtractID(object sender)
        {
            Button tempBtn = (Button)sender;
            string btnName = tempBtn.Name;
            string numberString = btnName.Remove(0, 4);
            return int.Parse(numberString);
        }

        internal static List<int> ConvertStringToList(string element)
        {
            string[] serializedList = element.Split(null);
            List<int> desrializedList = new List<int> { };
            foreach (string item in serializedList)
            {
                desrializedList.Add(int.Parse(item));
            }
            return desrializedList;
        }

        internal static List<Hitch.HitchType> ConvertIntListToHitchList(List<int> intList)
        {
            List<Hitch.HitchType> hitchList = new List<Hitch.HitchType> { };
            foreach (var integer in intList)
            {
                hitchList.Add((Hitch.HitchType)integer);
            }
            return hitchList;
        }

        

    }
}
