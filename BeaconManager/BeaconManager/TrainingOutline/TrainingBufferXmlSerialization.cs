using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using BeaconManager.Core.Serialization;

namespace BeaconManager.TrainingOutline
{
    /// <summary>
    /// 
    /// <RootNode>
    ///     <elementNode>
    ///         </>
    ///         </>
    ///         </>
    ///     </elementNode>
    ///     <elementNode>
    ///         </>
    ///         </>
    ///         </>
    ///     </elementNode>
    /// </RootNode>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class TrainingBufferSerialization
    {
        private const string FILE_PATH = "C:\\Users\\danie\\source\\repos\\Training.xml";
        private const string ROOT_NODE = "Outlines";
        private const string ELEMENT_NODE = "TrainingOutline";
        private const string NAME = "Name";
        private const string INCLUDED_STATIONS = "IncludedStations";

        /// <summary>
        /// Deserialize the XML document and creates a buffer.
        /// </summary>
        /// <returns> Training outline list buffer. </returns>
        public static List<TrainingOutline> Deserialize()
        {
            string xml = Serialization.LoadXmlToString(FILE_PATH);
            XDocument xDoc = XDocument.Parse(xml);

            List<TrainingOutline> buffer = new List<TrainingOutline> { };
            buffer.Add(TrainingOutlineFactory.CreateDefault()); // Default preset is always created -> even if it is not located on the XML file.

            var outlineNodeList = (from outline in xDoc.Descendants(ROOT_NODE).Elements(ELEMENT_NODE)
                       select new
                       {
                           Name = outline.Element(NAME).Value,
                           IncludedStations = outline.Element(INCLUDED_STATIONS).Value
                       }).ToList();
            
            foreach (var outline in outlineNodeList)
            {
                List<int> includedStations = ConvertStringToList(outline.IncludedStations);
                string name = outline.Name;

                TrainingOutline trainingOutline = TrainingOutlineFactory.Create(name, includedStations);
                buffer.Add(trainingOutline);
            }

            return buffer;
        }

        /// <summary>
        /// Serialize a specific training outline to the XML file.
        /// </summary>
        /// <param name="outline"> The training outline that will be serialized to the XML file. </param>
        public static void SerializeOutline(TrainingOutline outline)
        {
            XDocument xDoc = XDocument.Parse(Serialization.LoadXmlToString(FILE_PATH));

            XElement xOutline = new XElement(ELEMENT_NODE,
                new XElement(NAME, outline.Name),
                new XElement(INCLUDED_STATIONS, ConvertListToString(outline.IncludedStations)));

            xDoc.Root.Add(xOutline);
            xDoc.Save(FILE_PATH);
        }

        /// <summary>
        /// Removes a specific training outline from the XML file.
        /// </summary>
        /// <param name="target"> The name of the training outline we wish to remove. </param>
        public static void RemoveOutline(string target)
        {
            XDocument xDoc = XDocument.Parse(Serialization.LoadXmlToString(FILE_PATH));

            xDoc.Descendants(ROOT_NODE).Elements(ELEMENT_NODE)
                .Where(TrainingOutline => TrainingOutline.Element(NAME).Value == target).Remove();

            xDoc.Save(FILE_PATH);
        }

        /// <summary>
        /// Converts a string with the PC stations to a corresponding int list.
        /// </summary>
        /// <param name="element"> A string with the PC stations representation. </param>
        /// <returns> PC stations numbers list. </returns>
        private static List<int> ConvertStringToList(string element)
        {
            string[] stations = element.Split(null);
            List<int> includedStations = new List<int> { };
            foreach (string station in stations)
            {
                includedStations.Add(int.Parse(station));
            }
            return includedStations;
        }
        /// <summary>
        /// Converts an int list to a string with the PC stations divided ny whitespace.
        /// </summary>
        /// <param name="includedStations"> PC stations numbers list. </param>
        /// <returns> A string with the PC stations representation. </returns>
        private static string ConvertListToString(List<int> includedStations)
        {
            string temp = "";
            foreach (int stationID in includedStations)
            {
                temp += stationID.ToString() + " ";
            }
            temp = temp.Trim();
            return temp;
        }

    }
}
