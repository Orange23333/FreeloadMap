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
#warning 这里可以在移动检测图像左边缘是否超出canvas的左、上边缘，然后全体移动canvas。
    public partial class PictureItemControl
    {
        public event EventHandler MoveFinished;

        private Point move_RelativePosition;

        private Timer timer_move = new Timer()
        {
            Interval = 50 //20FPS
        };

        private void Init_Move()
        {
            timer_move.Elapsed += move_Elapsed;
        }

        private void Button_MovePicture_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            move_RelativePosition = Mouse.GetPosition(Button_MovePicture);
            timer_move.Start();
        }

        private void Button_MovePicture_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StopMove();
        }

        private void StopMove()
        {
            timer_move.Stop();

            this.MoveFinished?.Invoke(this, EventArgs.Empty);
        }

        private void move_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Point mousePosition = Mouse.GetPosition(pictureItemContainer.Container_Canvas);
                this.BeginInit();
                Canvas.SetLeft(this, mousePosition.X - move_RelativePosition.X);
                Canvas.SetTop(this, mousePosition.Y - move_RelativePosition.Y);
                this.EndInit();

                if (Mouse.LeftButton == MouseButtonState.Released)
                {
                    StopMove();
                    //return;
                }
            });
        }
    }
}
