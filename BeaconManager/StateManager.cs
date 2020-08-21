using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

namespace BeaconManager
{
    public static class StateManager
    {
        private static readonly Color DEFAULT_COLOR = (Color)ColorConverter.ConvertFromString("#FFDDDDDD");
        private static readonly Color BLOCKED_COLOR = (Color)ColorConverter.ConvertFromString("#FFE8FF00");
        private static readonly Color BROKEN_COLOR = (Color)ColorConverter.ConvertFromString("#FF812020");

        private const byte DEFAULT_STATE = 0;
        private const byte BLOCKED_STATE = 1;
        private const byte BROKEN_STATE = 2;

        private static byte _currentState = 0;

        private static void SetDefault(Button button)
        {
            ChangeButtonColor(button, DEFAULT_COLOR);
        } //0
        private static void SetBlocked(Button button)
        {
            ChangeButtonColor(button, BLOCKED_COLOR);
        } //1
        private static void SetBroken(Button button)
        {
            ChangeButtonColor(button, BROKEN_COLOR);
        } //2

        public static void CheckState(Button button)
        {
             switch (_currentState)
             {
                 case DEFAULT_STATE:
                     SetDefault(button);
                     break;
                 case BLOCKED_STATE:
                     SetBlocked(button);
                     break;
                 case BROKEN_STATE:
                     SetBroken(button);
                     break;
             }
        }

        public static void SetCurrentState(byte state)
        {
            _currentState = state;
        }
        public static byte GetCurrentState()
        {
            return _currentState;
        }

        private static void ChangeButtonColor(Button button, Color color)
        {
            // Changes the button color
            button.Background = new SolidColorBrush(color);
        }

    }
}
