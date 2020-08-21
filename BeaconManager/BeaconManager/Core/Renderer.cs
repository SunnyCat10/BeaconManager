using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BeaconManager.Constants;

namespace BeaconManager.Core
{
    internal class Renderer
    {
        private List<Button> ControlsList { get; set; }
        
        /// <summary>
        /// Renderer constructor.
        /// </summary>
        /// <param name="btnList"> All the controls that should be rendered. </param>
        public Renderer(List<Button> btnList)
        {
            ControlsList = btnList;
        }

        /// <summary>
        /// Renders the Outline to the UI.
        /// </summary>
        /// <param name="userSelection"> The stations that should be rendered as enabled. </param>
        public void RenderOutline(List<int> userSelection)
        {
            RenderDisabled();
            RenderEnabled(userSelection);
        }

        /// <summary>
        /// Renders all the buttons to the disabled station color.
        /// </summary>
        internal void RenderDisabled()
        {
            foreach (var button in ControlsList)
            {
                var brush = Utilities.Converter.HexColorToBrush(UIColors.BLOCKED_COLOR);
                button.Background = brush;
            }
        }

        /// <summary>
        /// Renders the buttons that are enabled by the outline.
        /// </summary>
        /// <param name="userSelection"> List of the stations that are enabled by the outline. </param>
        private void RenderEnabled(List<int> userSelection)
        {
            foreach (int station in userSelection)
            {
                var brush = Utilities.Converter.HexColorToBrush(UIColors.DEFAULT_COLOR);
                ControlsList[station - 1].Background = brush;
            }
        }
        
    }
}
