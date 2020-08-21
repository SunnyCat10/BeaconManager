using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

/// <summary>
/// @Author Daniel Dovgun
/// @Version 16/5/2020
/// </summary>
namespace BeaconManager.Core.Selection
{
    internal class SelectionBuffer
    {
        private Button[] BtnArray { get; set; }
        private bool[] IsSelectedArray { get; set; }

        internal SelectionBuffer(List<Button> stationCtrlList)
        {
            BtnArray = new Button[Constants.Settings.STATIONS_AMOUNT];
            BtnArray = stationCtrlList.ToArray();

            IsSelectedArray = new bool[Constants.Settings.STATIONS_AMOUNT];
            UnselectAll();
        }

        internal void Select(int stationIndex)
        {
            --stationIndex;
            IsSelectedArray[stationIndex] = true;
        }

        internal void Unselect(int stationIndex)
        {
            --stationIndex;
            IsSelectedArray[stationIndex] = false;
        }

        internal void UnselectAll()
        {
            for (int i = 0; i < Constants.Settings.STATIONS_AMOUNT; i++)
            {
                IsSelectedArray[i] = false;
            }
        }

        internal bool IsSelected(int stationIndex)
        {
            --stationIndex;
            return IsSelectedArray[stationIndex];
        }

        internal Button GetButton(int stationIndex)
        {
            --stationIndex;
            return BtnArray[stationIndex];
        }

        internal List<Button> GetAllSelected()
        {
            var selectionList = new List<Button> { };

            for (int i = 0; i < IsSelectedArray.Length; i++)
            {
                if (IsSelectedArray[i])
                {
                    selectionList.Add(BtnArray[i]);
                }
            }

            return selectionList;
        }
    }
}
