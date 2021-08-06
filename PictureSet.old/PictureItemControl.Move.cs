using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace PictureSet
{
    public partial class PictureItemControl
    {
        public event EventHandler MoveFinished;

        private Timer timer_move = new Timer()
        {
            Interval = 50 //20FPS
        };

        private void Init_Move()
        {
            timer_move.Elapsed += move_Elapsed;
        }

        private void button_MovePicture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            timer_move.Start();
        }

        private void button_MovePicture_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StopMove();
        }

        private void StopMove()
        {
            timer_move.Stop();

            this.MoveFinished.Invoke(this, EventArgs.Empty);
        }

        private void move_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Point mousePosition = Mouse.GetPosition(container);
                this.BeginInit();
                Canvas.SetLeft(this, mousePosition.X);
                Canvas.SetTop(this, mousePosition.Y);
                this.EndInit();

                if (Mouse.LeftButton == MouseButtonState.Released || button_MovePicture.IsMouseCaptureWithin)
                {
                    StopMove();
                    //return;
                }
            });
        }
    }
}
