using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BeaconManager.TrainingOutline;
using BeaconManager.Core;
using BeaconManager.Beacon;
using BeaconManager.Console;
using BeaconManager.Core.Selection;

namespace BeaconManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const byte DEFAULT_STATE = 0;
        private const byte BLOCKED_STATE = 1;
        private const byte BROKEN_STATE = 2;

        private static List<Button> buttonList = new List<Button> { };
        private static Renderer renderer;
        private static OutlineScroll outlineScroll;

        private static CatalogConsole catalogConsole;

        private ButtonSelecter buttonSelecter;
        private Core.Selection.SingleButton singleBtnSelecter;

        private RoutedEventHandler btnSelectHandler;
        private RoutedEventHandler singleBtnSelectHandler;

        public MainWindow()
        {
            InitializeComponent();
            FillButtonList();
            TrainingOutlineBuffer.Load();
            BeaconBuffer.Load();
            ButtonOutlineManager.DefaultOutline();


            var console1 = (Label)FindName("Debug");
            var console2 = (Label)FindName("Console_2");
            var txtCompID = (TextBox)FindName("txtbox_computer_id");
            var txtCompName = (TextBox)FindName("txtbox_computer_name");
            var txtScreenID = (TextBox)FindName("txtbox_screen_id");
            var txtScreenName = (TextBox)FindName("txtbox_screen_name");
            catalogConsole = new CatalogConsole(console1, console2, txtCompID, txtCompName,
                txtScreenID, txtScreenName);

            
            buttonSelecter = new ButtonSelecter();
            singleBtnSelecter = new Core.Selection.SingleButton();

            SetUpEventHandlers();

            Subscribe();
            UnsubscribeBtns(false);

            renderer = new Renderer(buttonList);
            outlineScroll = new OutlineScroll(renderer);

            //debug(BeaconBuffer.Buffer[0].Catalog.PrintScreenCatalog());
            //printConsole2(BeaconBuffer.Buffer[0].Catalog.PrintComputerCatalog());
            UpdateOutlineTextbox();
        }

        private void FillButtonList()
        { 
            for (int i = 1; i <= Constants.Settings.STATIONS_AMOUNT; i++)
            {
                string str = "btn_" + i;
                Button temp = (Button)FindName(str);
                buttonList.Add(temp);
            }
        }

        private void Station_Button_Click(object sender, RoutedEventArgs e)
        {
            StateManager.CheckState((Button)sender);
        }

        private void Subscribe()
        {
            btn_catalog_view.Click += (object sender, RoutedEventArgs e) =>
            {
                UnsubscribeBtns(true);
                SubscribeBtns(false);
            };
            btn_catalog_update_computer.Click += (object sender, RoutedEventArgs e) => 
            {
                catalogConsole.SetComputerCatalog(singleBtnSelecter.CurrentSelection);       
            };
            btn_catalog_update_screen.Click += (object sender, RoutedEventArgs e) =>
            {
                catalogConsole.SetScreenCatalog(singleBtnSelecter.CurrentSelection);
            };
            btn_save_outline.Click += (object sender, RoutedEventArgs e) => 
            {
                foreach (int ID in buttonSelecter.BtnSelectionBuffer)
                {
                    string btnName = Core.Utilities.Converter.IdToStationBtn(ID);
                    Button btn = (Button)FindName(btnName);
                    buttonSelecter.ClearColoredButton(btn);
                }
                string outlineName = txtbox_outline.Text;
                var trainingOutline = TrainingOutlineFactory.Create(outlineName , buttonSelecter.BtnSelectionBuffer);
                TrainingOutlineBuffer.Add(trainingOutline);
                buttonSelecter.ClearBuffer();
                outlineScroll.Lengthened();
                debug(buttonSelecter.ToString());
            };
            btn_next.Click += (object sender, RoutedEventArgs e) =>
            {
                outlineScroll.Next();
                UpdateOutlineTextbox();
            };
            btn_previous.Click += (object sender, RoutedEventArgs e) =>
            {
                outlineScroll.Previous();
                UpdateOutlineTextbox();
            };
            btn_remove_outline.Click += (object sender, RoutedEventArgs e) =>
            {
                string outlineName = txtbox_outline.Text;
                if (TrainingOutlineBuffer.Remove(outlineName))
                {
                    /* 
                     * There is no reason to shrink our scroll if nothing was removed.
                     * So only if we removed an outline, our scroll will shrink.
                     */
                    outlineScroll.Shortened();
                }
            };
        }

        // Deprecated 
        private void SubscribeStationsToEvent()
        {
            btn_1.Click += Station_Button_Click;
            btn_2.Click += Station_Button_Click;
            btn_3.Click += Station_Button_Click;
            btn_4.Click += Station_Button_Click; 
            btn_5.Click += Station_Button_Click;
            btn_6.Click += Station_Button_Click;
        }

        private void debug(string str)
        {
            Label debug = (Label)FindName("Debug");
            debug.Content = str;
        }
        private void printConsole2(string message)
        {
            var console2 = (Label)FindName("Console_2");
            console2.Content = message;        
        }


        private void Btn_add_outline_Click(object sender, RoutedEventArgs e)
        {
            UnsubscribeBtns(false);
            SubscribeBtns(true);

            renderer.RenderDisabled();
            txtbox_outline.TextAlignment = TextAlignment.Right;
            txtbox_outline.Text = "";
            var color = (Color)ColorConverter.ConvertFromString(Constants.UIColors.YELLOW_TEXT_COLOR);
            txtbox_outline.Foreground = new SolidColorBrush(color);
        }

        /// <summary>
        /// Updates the Outline Text box element.
        /// </summary>
        private void UpdateOutlineTextbox()
        {
            txtbox_outline.Text = outlineScroll.CurrentScrollOutline();
            txtbox_outline.TextAlignment = TextAlignment.Center;
            var color = (Color)ColorConverter.ConvertFromString(Constants.UIColors.WHITE_TEXT_COLOR);
            txtbox_outline.Foreground = new SolidColorBrush(color); 
        }

        private void UnsubscribeBtns(bool multiSelection)
        {
            if (multiSelection == true)
            {
                for (int i = 0; i < Constants.Settings.STATIONS_AMOUNT; i++)
                {
                    buttonList[i].Click -= btnSelectHandler;
                }
            }
            else
            {
                for (int i = 0; i < Constants.Settings.STATIONS_AMOUNT; i++)
                {
                    buttonList[i].Click -= singleBtnSelectHandler;
                }
            }
        }
        private void SubscribeBtns(bool multiSelection)
        {
            if (multiSelection == true)
            {
                for (int i = 0; i < Constants.Settings.STATIONS_AMOUNT; i++)
                {
                    buttonList[i].Click += btnSelectHandler;
                }
            }
            else
            {
                for (int i = 0; i < Constants.Settings.STATIONS_AMOUNT; i++)
                {
                    buttonList[i].Click += singleBtnSelectHandler;
                }
            }
        }

        private void SetUpEventHandlers()
        {
            btnSelectHandler = (object sender, RoutedEventArgs e) =>
            {
                buttonSelecter.MarkSelectedButton(sender);
                
            };
            singleBtnSelectHandler = (object sender, RoutedEventArgs e) =>
            {
                singleBtnSelecter.SelectButton(sender);
                catalogConsole.Update(singleBtnSelecter.CurrentSelection);
            };
        }
    }
}
