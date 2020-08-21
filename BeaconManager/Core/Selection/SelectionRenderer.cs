using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

/// <summary>
/// @Author Daniel Dovgun
/// @Version 5/16/2020
/// </summary>
namespace BeaconManager.Core.Selection
{
    internal static class SelectionRenderer
    {
        internal static void Render(object sender)
        {
            Color selectedColor = (Color)ColorConverter.ConvertFromString(Constants.UIColors.SELECTED_COLOR);
            Button btn = (Button)sender;
            btn.Background = new SolidColorBrush(selectedColor);
        }

        internal static void Unrender(object sender)
        {
            Color selectedColor = (Color)ColorConverter.ConvertFromString(Constants.UIColors.DEFAULT_COLOR);
            Button btn = (Button)sender;
            btn.Background = new SolidColorBrush(selectedColor);
        }
    }
}
