using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

/// <summary>
/// @Author Daniel Dovgun
/// @Date 5/16/2020
/// </summary>
namespace BeaconManager.Core.Selection
{
    /// <summary>
    /// Multi-button selection mode.
    /// </summary>
    internal class MultiButton 
    {
        private SelectionBuffer SelectionBuffer { get; set; }

        /// <summary>
        /// Multi-button selection mode constructor.
        /// </summary>
        /// <param name="btnList"> Button list which represetns the computer stations. </param>
        internal MultiButton(List<Button> btnList)
        {
            SelectionBuffer = new SelectionBuffer(btnList);
        }

        /// <summary>
        /// Clears all the selection.
        /// </summary>
        internal void Clear()
        {
            SelectionBuffer.UnselectAll();
            for (int i = 1; i <= Constants.Settings.STATIONS_AMOUNT; i++)
            {
                SelectionRenderer.Unrender(SelectionBuffer.GetButton(i));
            }
        }

        /// <summary>
        /// Selects a button.
        /// </summary>
        /// <param name="sender"> Clicked button. </param>
        internal void SelectButton(object sender)
        {
            int selectedIndex = Utilities.Converter.ExtractID(sender);
            if (SelectionBuffer.IsSelected(selectedIndex))
            {
                UnselectButton(sender);
            }
            else
            {
                SelectionBuffer.Select(selectedIndex);
                SelectionRenderer.Render(sender);             
            }
        }

        /// <summary>
        /// Unselects a button.
        /// </summary>
        /// <param name="sender"> Clicked button. </param>
        internal void UnselectButton(object sender)
        {
            int selectedIndex = Utilities.Converter.ExtractID(sender);
            SelectionBuffer.Unselect(selectedIndex);
            SelectionRenderer.Unrender(sender);
        }

        /// <summary>
        /// Get all the selected buttons in a station`s ID list format.
        /// </summary>
        /// <returns> selected buttons in a station`s ID list format </returns>
        internal List<int> GetSelection()
        {
            var selectedIDs = new List<int> { };

            var selectedBtnList = SelectionBuffer.GetAllSelected();
            foreach (var btn in selectedBtnList)
            {
                var temp = Utilities.Converter.ExtractID(btn);
                selectedIDs.Add(temp);
            }

            return selectedIDs;
        }

    }
}
