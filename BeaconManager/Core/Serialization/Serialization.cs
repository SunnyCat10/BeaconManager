using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BeaconManager.Core.Serialization
{
    internal static class Serialization
    {
        /// <summary>
        /// Converts the XML file to a string format.
        /// </summary>
        /// <param name="path"> The path of the XML file. </param>
        /// <returns> String representation of the XML file. </returns>
        internal static string LoadXmlToString(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(filePath);

            string xml = xmlDoc.OuterXml.ToString();
            return xml;
        }
    }


}
