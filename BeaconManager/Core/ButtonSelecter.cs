using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using BeaconManager.Core;

namespace BeaconManager.Core
{
    public class ButtonSelecter
    {
        public List<int> BtnSelectionBuffer { get; private set; }
        public bool Full { get; private set; }
        

        /// <summary>
        /// ButtonSelecter object constructor.
        /// </summary>
        public ButtonSelecter()
        {
            BtnSelectionBuffer = new List<int> { };
        }

        public void MarkSelectedButton(object sender)
        {
            bool alreadySelected = SearchBuffer( Utilities.Converter.ExtractID(sender) );
            ColorButton(sender, alreadySelected);
            Full = (BtnSelectionBuffer.Count == 0) ? false : true;
        }

        /// <summary>
        /// Clears the selection Buffer.
        /// </summary>
        public void ClearBuffer()
        {
            BtnSelectionBuffer.Clear();
        }

        public void ClearColoredButton(Button btn)
        {
            ColorButton(btn, true);
        }

        /// <summary>
        /// Searches the buffer for a specific ID.
        /// If the ID is found -> the ID will be removed from the buffer.
        /// If the ID is not found -> The ID will be added to the buffer.
        /// </summary>
        /// <param name="stationId"> The ID of the PC station. </param>
        /// <returns> Was the ID of the station found in the buffer? </returns>
        private bool SearchBuffer(int stationId)
        {
            foreach(int ID in BtnSelectionBuffer)
            {
                if (ID == stationId)
                {
                    BtnSelectionBuffer.Remove(stationId);
                    return true;
                }
            }
            BtnSelectionBuffer.Add(stationId);
            return false;
        }

        /// <summary>
        /// Colors the selected Buttons.
        /// </summary>
        /// <param name="sender"> The event sender - PC station button. </param>
        /// <param name="selected"> Is the PC station already selected? </param> //TODO: UPDATE METHOD DESCRIPTION
        private void ColorButton(object sender, bool selected)
        {
            if(selected == false)
            {
                Color selectedColor = (Color)ColorConverter.ConvertFromString(Constants.UIColors.SELECTED_COLOR);
                Button btn = (Button)sender;
                btn.Background = new SolidColorBrush(selectedColor);
            }
            else
            {
                Color selectedColor = (Color)ColorConverter.ConvertFromString(Constants.UIColors.DEFAULT_COLOR);
                Button btn = (Button)sender;
                btn.Background = new SolidColorBrush(selectedColor);
            }
        }

        /// <summary>
        /// Returns the button selection buffer in a string format.
        /// </summary>
        /// <returns> Button selection buffer. </returns>
        public new string ToString()
        {
            string temp = "";
            foreach (int stationID in BtnSelectionBuffer)
            {
                temp += stationID.ToString() + " ";
            }
            //temp = temp.Trim();
            temp += '\n' + Full.ToString();
            return temp;
        }


    }
}
