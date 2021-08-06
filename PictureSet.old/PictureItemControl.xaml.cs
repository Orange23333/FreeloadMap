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

using FreeloadMap.Lib.Data;

namespace PictureSet
{
    /// <summary>
    /// PictureItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class PictureItemControl : UserControl
    {
        
		private Panel container;
        private PictureItemStructure pictureItemStructure;

        public ImageSource ImageSource
        {
            get { return image.Source; }
            set
            {
                this.BeginInit();
                try
                {
                    this.Width = value.Width;
                    this.Height = value.Height;
                    this.image.Source = value;
                }
                catch (Exception ex)
                {
                    this.EndInit();
                    throw ex;
                }
                this.EndInit();
            }
        }

        public PictureItemControl(Panel container)
        {
            InitializeComponent();

            this.container = container;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Init_Bindings_PictureItemStructure();
            Init_Move();
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            button_DeletePicture.Visibility = Visibility.Visible;
        }

        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
            button_DeletePicture.Visibility = Visibility.Hidden;
        }
    }
}
