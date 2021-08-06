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

using FreeloadMap.Lib.Data;

namespace PictureSet
{
    /// <summary>
    /// PictureItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class PictureItemControl : UserControl
    {
        private IPictureItemContainer pictureItemContainer;

        /// <remarks>
        /// 请使用这个来更新图片。
        /// </remarks>
        public ImageSource ImageSource
        {
            get { return Image_Picture.Source; }
            set
            {
                this.BeginInit();
                try
                {
                    this.Width = value.Width;
                    this.Height = value.Height;
                    this.Image_Picture.Source = value;
                }
                catch (Exception ex)
                {
                    this.EndInit();
                    throw ex;
                }
                this.EndInit();
            }
        }

        public PictureItemControl(IPictureItemContainer pictureItemContainer)
        {
            InitializeComponent();

            this.pictureItemContainer = pictureItemContainer;
        }

        private void ThisControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Init_Bindings_PictureItemStructure();
            Init_Move();
        }

        private void Button_DeletePicture_Click(object sender, RoutedEventArgs e)
        {
            pictureItemContainer.RemoveItem(pictureItemContainer.FindItem(this));
        }

        private void MainGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            AllControlButton_Set(Visibility.Visible);
        }
        private void MainGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            AllControlButton_Set(Visibility.Hidden);
        }
        private void AllControlButton_Set(Visibility visibility)
        {
            Button_DeletePicture.Visibility = visibility;
            Button_MovePicture.Visibility = visibility;
            Button_LiftingPictureZIndex.Visibility = visibility;
            Button_ReducePictureZIndex.Visibility = visibility;
        }

        private void Button_LiftingPictureZIndex_Click(object sender, RoutedEventArgs e)
        {
            this.BeginInit();
            try
            {
                //Panel.SetZIndex(Image_Picture, Panel.GetZIndex(Image_Picture) + 1);

                //this.PI_ZIndex += 1;

                this.pictureItemContainer.FindItem(this).ZIndex = this.PI_ZIndex + 1;
            }
            catch (Exception ex)
            {
                this.EndInit();
                throw ex;
            }
            this.EndInit();
        }

        private void Button_ReducePictureZIndex_Click(object sender, RoutedEventArgs e)
        {
            this.BeginInit();
            try
            {
                //Panel.SetZIndex(Image_Picture, Panel.GetZIndex(Image_Picture) - 1);

                //this.PI_ZIndex -= 1;

                this.pictureItemContainer.FindItem(this).ZIndex = this.PI_ZIndex - 1;
            }
            catch (Exception ex)
            {
                this.EndInit();
                throw ex;
            }
            this.EndInit();
        }
    }
}
