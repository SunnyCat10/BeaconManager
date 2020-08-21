using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

/// <summary>
/// @Author Daniel Dovgun
/// @Version 13/5/2020
/// </summary>
namespace BeaconManager.Core.Selection
{
    /// <summary>
    /// Single button selection.
    /// </summary>
    internal class SingleButton
    {
        private Button PreviousSelection { get; set; }
        internal int CurrentSelection { get; private set; }

        internal SingleButton()
        {
            PreviousSelection = null;
        }

        internal void SelectButton(object sender)
        {
            ColorButton(PreviousSelection, true);
            PreviousSelection = (Button)sender;
            ColorButton(sender, false);
            CurrentSelection = Utilities.Converter.ExtractID(PreviousSelection);
        }

        //TODO Same class exists at ButtonSelecter check if it can be reused
        private void ColorButton(object sender, bool selected)
        {
            if (PreviousSelection == null)
                return;
            if (selected == false)
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
    }
}
