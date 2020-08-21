using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using BeaconManager.Core.Serialization;
using BeaconManager.Beacon.Info;
using BeaconManager.Cataloging;

namespace BeaconManager.Beacon
{
    internal static class BeaconSerialization
    {
        private const string BEACON_PATH = "C:\\Users\\danie\\source\\repos\\BeaconData.xml";
        private const string BEACON_ROOT_NODE = "Beacons";
        private const string BEACON_ELEMENT_NODE = "Beacon";

        private const string INDEX = "Index";
        private const string CATALOG = "Catalog";
        private const string COMPUTER_CATALOG = "ComputerCatalog";
        private const string SCREEN_CATALOG = "ScreenCatalog";
        private const string CATALOG_ID = "CatalogID";
        private const string CATALOG_NAME = "CatalogName";
        private const string INFO = "Info";
        private const string ID = "ID";
        private const string NAME = "Name";
        private const string PERMISSION = "Permission";

        internal static List<BeaconStation> Deserialize()
        {
            string xml = Serialization.LoadXmlToString(BEACON_PATH);
            XDocument xDoc = XDocument.Parse(xml);

            var buffer = new List<BeaconStation> { };

            var beaconNodeList = (from beacon in xDoc.Descendants(BEACON_ROOT_NODE).Elements(BEACON_ELEMENT_NODE)
                                  select new
                                  {
                                      Index = beacon.Element(INDEX).Value,
                                      Catalog = new
                                      {
                                          ComputerCatalog = new
                                          {
                                              CatalogID = beacon.Element(CATALOG).Element(COMPUTER_CATALOG).Element(CATALOG_ID).Value,
                                              CatalongName = beacon.Element(CATALOG).Element(COMPUTER_CATALOG).Element(CATALOG_NAME).Value
                                          },
                                          ScreenCatalog = new
                                          {
                                              CatalogID = beacon.Element(CATALOG).Element(SCREEN_CATALOG).Element(CATALOG_ID).Value,
                                              CatalongName = beacon.Element(CATALOG).Element(SCREEN_CATALOG).Element(CATALOG_NAME).Value
                                          }
                                      },
                                      Info = new
                                      {
                                          ID = beacon.Element(INFO).Element(ID).Value,
                                          Name = beacon.Element(INFO).Element(NAME).Value,
                                          Permission = beacon.Element(INFO).Element(PERMISSION).Value
                                      }
                                  }).ToList();

            foreach (var beacon in beaconNodeList)
            {
                var stationInfo = new StationInfo(beacon.Info.ID, beacon.Info.Name,
                    beacon.Info.Permission);

                var computerCatalog = new CatalogEntity(beacon.Catalog.ComputerCatalog.CatalogID,
                    beacon.Catalog.ComputerCatalog.CatalongName);
                var screenCatalog = new CatalogEntity(beacon.Catalog.ScreenCatalog.CatalogID,
                    beacon.Catalog.ScreenCatalog.CatalongName);

                var catalogStation = new CatalogStation(computerCatalog, screenCatalog);

                var beaconStation = BeaconFactory.Create(stationInfo, catalogStation);
                buffer.Add(beaconStation);
            }

            return buffer;
        }

        internal static void SerializeBeacon(BeaconStation beaconStation)
        {
            XDocument xDoc = XDocument.Parse(Serialization.LoadXmlToString(BEACON_PATH));

            XElement xBeacon = new XElement(BEACON_ELEMENT_NODE,
                new XElement(INDEX, beaconStation.Index),
                new XElement(CATALOG,
                    new XElement(COMPUTER_CATALOG,
                        new XElement(CATALOG_ID, beaconStation.Catalog.ComputerCatalog.CatalogID),
                        new XElement(CATALOG_NAME, beaconStation.Catalog.ComputerCatalog.CatalogName)),
                    new XElement(SCREEN_CATALOG,
                        new XElement(CATALOG_ID, beaconStation.Catalog.ScreenCatalog.CatalogID),
                        new XElement(CATALOG_NAME, beaconStation.Catalog.ScreenCatalog.CatalogName)),
                new XElement(INFO,
                    new XElement(ID, beaconStation.Info.ID),
                    new XElement(NAME, beaconStation.Info.Name),
                    new XElement(PERMISSION, beaconStation.Info.Permission))));

            xDoc.Root.Add(xBeacon);
            xDoc.Save(BEACON_PATH);
        }

        internal static void SerializeUpdatedComputerCatalog(int index, CatalogEntity updatedComputerCatalog)
        {
            XDocument xDoc = XDocument.Parse(Serialization.LoadXmlToString(BEACON_PATH));

            var beaconToUpdateList = from beacon in xDoc.Descendants(BEACON_ELEMENT_NODE)
                                 where beacon.Element(INDEX).Value == index.ToString()
                                 select beacon;

            foreach (var beacon in beaconToUpdateList)
            {
                beacon.Element(CATALOG).Element(COMPUTER_CATALOG)
                    .SetElementValue(CATALOG_ID, updatedComputerCatalog.CatalogID);
                beacon.Element(CATALOG).Element(COMPUTER_CATALOG)
                    .SetElementValue(CATALOG_NAME, updatedComputerCatalog.CatalogName);
            }

            xDoc.Save(BEACON_PATH);
        }

        internal static void SerializeUpdatedScreenCatalog(int index, CatalogEntity updatedScreenCatalog)
        {
            XDocument xDoc = XDocument.Parse(Serialization.LoadXmlToString(BEACON_PATH));

            var beaconToUpdateList = from beacon in xDoc.Descendants(BEACON_ELEMENT_NODE)
                                     where beacon.Element(INDEX).Value == index.ToString()
                                     select beacon;

            foreach (var beacon in beaconToUpdateList)
            {
                beacon.Element(CATALOG).Element(SCREEN_CATALOG)
                    .SetElementValue(CATALOG_ID, updatedScreenCatalog.CatalogID);
                beacon.Element(CATALOG).Element(SCREEN_CATALOG)
                    .SetElementValue(CATALOG_NAME, updatedScreenCatalog.CatalogName);
            }

            xDoc.Save(BEACON_PATH);
        }
    }
}
