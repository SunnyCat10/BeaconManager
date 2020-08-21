using BeaconManager.Beacon;
using BeaconManager.Cataloging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

/// <summary>
/// @Author Daniel Dovgun
/// @Date 13/5/2020
/// </summary>
namespace BeaconManager.Console
{
    internal class CatalogConsole
    {
        private Label Console1 { get; set; }
        private Label Console2 { get; set; }

        private TextBox TxtComputerID { get; set; }
        private TextBox TxtComputerName { get; set; }
        private TextBox TxtScreenID { get; set; }
        private TextBox TxtScreenName { get; set; }

        private const string COMPUTER_CATALOG = ":מחשב";
        private const string SCREEN_CATALOG = ":מסך";
        private const string CATALOG_ID = ":מקט";
        private const string CATALOG_NAME = ":שם פריט";

        internal CatalogConsole(Label console1, Label console2, TextBox txtComputerID,
            TextBox txtComputerName, TextBox txtScreenID, TextBox txtScreenName)
        {
            Console1 = console1;
            Console2 = console2;

            TxtComputerID = txtComputerID;
            TxtComputerName = txtComputerName;
            TxtScreenID = txtScreenID;
            TxtScreenName = txtScreenName;
        }

        internal void Update(int selectedStation)
        {
            var computerCatalog = BeaconBuffer.Buffer[selectedStation - 1].Catalog.ComputerCatalog;
            var screenCatalog = BeaconBuffer.Buffer[selectedStation - 1].Catalog.ScreenCatalog;

            PrintToConsole(computerCatalog, screenCatalog);
        }

        internal void UpdateComputerCatalog(int selectedStation)
        {
            TxtComputerID.Text = BeaconBuffer.Buffer[selectedStation].Catalog.ComputerCatalog.CatalogID;
            TxtComputerName.Text = BeaconBuffer.Buffer[selectedStation].Catalog.ComputerCatalog.CatalogName;
        }
        internal void UpdateScreenCatalog(int selectedStation)
        {
            TxtScreenID.Text = BeaconBuffer.Buffer[selectedStation].Catalog.ScreenCatalog.CatalogID;
            TxtScreenName.Text = BeaconBuffer.Buffer[selectedStation].Catalog.ScreenCatalog.CatalogName;
        }
        internal void UpdateBothConsoles(int selectedStation)
        {
            UpdateComputerCatalog(selectedStation);
            UpdateScreenCatalog(selectedStation);
        }

        internal void SetComputerCatalog(int selectedStation)
        {
            var compCatalogID = TxtComputerID.Text;
            var compCatalogName = TxtComputerName.Text;
            var tempCompCatalog = new CatalogEntity(compCatalogID, compCatalogName);
            BeaconBuffer.UpdateComputerCatalog(selectedStation, tempCompCatalog);
        }
        internal void SetScreenCatalog(int selectedStation)
        {
            var screenCatalogID = TxtScreenID.Text;
            var screenCatalogName = TxtScreenName.Text;
            var tempScreenCatalog = new CatalogEntity(screenCatalogID, screenCatalogName);
            BeaconBuffer.UpdateComputerCatalog(selectedStation, tempScreenCatalog);
        }

        private string PrintComputerCatalog(CatalogEntity computerCatalog)
        {
            string output = "";
            output += COMPUTER_CATALOG + '\n' + "=-=-=-=-=" + '\n' + computerCatalog.CatalogID +
                '\t' + "       " + CATALOG_ID +
                '\n' + computerCatalog.CatalogName + '\t' + CATALOG_NAME;
            return output;
        }

        private string PrintScreenCatalog(CatalogEntity screenCatalog)
        {
            return SCREEN_CATALOG + '\n' + "=-=-=-=-=" + '\n' + screenCatalog.CatalogID + 
                '\t' + "       " + CATALOG_ID +
                '\n' + screenCatalog.CatalogName + '\t' + CATALOG_NAME;
        }

        private void PrintToConsole(CatalogEntity computer, CatalogEntity screen)
        {
            Console1.Content = PrintComputerCatalog(computer);
            Console2.Content = PrintScreenCatalog(screen);
        }
    }
}
