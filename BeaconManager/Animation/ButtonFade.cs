using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

using System.Timers;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Microsoft.Office.Core;

using BeaconManager.Core;

/// <summary>
/// @Author Daniel Dovgun
/// @Date 30/5/2020
/// </summary>
namespace BeaconManager.Animation
{
    /// <summary>
    /// Animation for button fading.
    /// </summary>
    internal class ButtonFade
    {
        private const double VISIBLE_OPACITY = 1.0;
        private const double INVISIBLE_OPACITY = 0.0;
        private const double FADE_OUT_DELTA = -0.1; 
        private const double FADE_IN_DELTA = 0.1;

        internal List<Control> ControlList { get; set; }
        internal DispatcherTimer AnimationTimer { get; private set; } //set get to private
        internal double Opacity { get; private set; } 
        internal double OpacityDelta { get; private set; }

        private const int animationFrameLength = 10;
        private int animationCurretFrame = 0;

        /// <summary>
        /// Fading Animation constructor.
        /// </summary>
        /// <param name="buttonList"> List of Buttons for the Animation to play on. </param>
        internal ButtonFade(List<Control> controlList)
        {
            ControlList = controlList;
            DispatcherTimerInitializer();
        }

        /// <summary>
        /// Fading out animation.
        /// </summary>
        internal void FadeOut() // 1.0 --> 0.0
        {
            Opacity = VISIBLE_OPACITY;
            OpacityDelta = FADE_OUT_DELTA;
            AnimationTimer.Start();
        }

        /// <summary>
        /// Fading in animation.
        /// </summary>
        internal void FadeIn() // 0.0 --> 1.1
        {
            Opacity = INVISIBLE_OPACITY;
            OpacityDelta = FADE_IN_DELTA;
            AnimationTimer.Start();
        }

        /// <summary>
        /// Helper function to initialize the DispatcherTimer object.
        /// </summary>
        private void DispatcherTimerInitializer()
        {
            AnimationTimer = new DispatcherTimer(DispatcherPriority.Render, Application.Current.Dispatcher);
            AnimationTimer.Tick += new EventHandler(FadingAnimation);
            AnimationTimer.Interval += new TimeSpan(0, 0, 0, 0, 30);
        }

        /// <summary>
        /// Fading animation event handler fot the dispatcher timer.S
        /// </summary>
        /// <param name="sender"> Event handler sender. </param>
        /// <param name="e"> Event handler parameters. </param>
        private void FadingAnimation(object sender, EventArgs e)
        {
            if (animationCurretFrame == animationFrameLength)
            {
                AnimationTimer.Stop();
                animationCurretFrame = 0;
            }
            else
            {
                Opacity += OpacityDelta;
                foreach (var control in ControlList)
                    Renderer.RenderOpacity(control, Opacity);

                ++animationCurretFrame;
            }
        }

        //TODO: Move to static rendering class.
        private static void RenderOpacity(Button button, double opacity)
        {
            button.Opacity = opacity;
        }
        
    }
}
