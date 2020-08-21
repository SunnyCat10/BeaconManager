using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BeaconManager.Constants;

namespace BeaconManager.TrainingOutline
{
    public static class ButtonOutlineManager
    {
        private static List<Button> btnList;
        private static Window mainWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);
        public static int BufferIndex { get; private set; }

        private const string BTN_PREFIX = "btn_";

        public static void DefaultOutline()
        {
            BufferIndex = 0;
            btnList = new List<Button> { };
            AddButtons();
            OutlineButtons(TrainingOutlineBuffer.Buffer[BufferIndex]);
        }

        public static bool Next() 
        {
            int nextIndex = BufferIndex + 1;
            if (nextIndex > (TrainingOutlineBuffer.Buffer.Count - 1) )
                return false;
            else
            {
                nextIndex = ++BufferIndex;
                OutlineButtons(TrainingOutlineBuffer.Buffer[nextIndex]);
                return true;
            }
        }

        public static bool Previous()
        {
            if (BufferIndex <= 0)
                return false;
            else
            {
                int previousIndex = --BufferIndex;
                OutlineButtons(TrainingOutlineBuffer.Buffer[previousIndex]);
                return true;
            }
        }

        public static void OutlineButtons(TrainingOutline outline)
        {
            DeactivateAll();
            List<int> stationsOutline = outline.IncludedStations;

            foreach (var station in stationsOutline)
            {
                var stationButton = FindButton(station);
                ColorButton(stationButton, UIColors.DEFAULT_COLOR);
            }           
        }

        public static string ToString()
        {
            return TrainingOutlineBuffer.Buffer[BufferIndex].Name;
        }

        /// <summary>
        /// Find button of the type btn_x by the index (x).
        /// </summary>
        /// <param name="index"> The x index of the button (btn_x). </param>
        /// <returns> The button. </returns>
        private static Button FindButton(int index)
        {
            string target = BTN_PREFIX + index;
            return (Button)mainWindow.FindName(target);
        }

        /// <summary>
        /// Add all the PC stations to the buttons list.
        /// </summary>
        private static void AddButtons()
        {
            for (int i = 1; i <= Settings.STATIONS_AMOUNT; i++)
            {
                var Button = FindButton(i);
                if (Button == null)
                    continue;
                btnList.Add(Button);
            }
        }

        private static void ColorButton(Button button, string targetColor)
        {
            var color = (Color)ColorConverter.ConvertFromString(targetColor);
            var brush = new SolidColorBrush(color);
            button.Background = brush;
        }

        private static void DeactivateAll()
        {
            // Deactivate event handlers
            foreach (var button in btnList)
            {
                ColorButton(button, UIColors.BLOCKED_COLOR);
            }
        }



    }
}
