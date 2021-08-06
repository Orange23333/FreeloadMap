using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

#error 直接ps开整

namespace PictureSet
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string BasePath { get; set; } = null;

        private Microsoft.Win32.OpenFileDialog openFileDialog;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            openFileDialog = new Microsoft.Win32.OpenFileDialog();

            pictureItems = new SortedSet<PictureItem>(new PictureItem.PictureItemComparer());

            Init_Project();

#warning DEBUG
            PictureItemControl pic = new PictureItemControl(pictureCanvas);
            pictureCanvas.Children.Add(pic);
            pic.BeginInit();
            pic.Width = 200;
            pic.Height = 200;
            Canvas.SetLeft(pic, 0);
            Canvas.SetTop(pic, 0);
            pic.EndInit();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void PictureCanvas_Drop(object sender, DragEventArgs e)
        {
            int i, _8i;
            Point mousePosition = Mouse.GetPosition(this.pictureCanvas);
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);

            this.BeginInit();

            try
            {
                for (i = 0; i < paths.Length; i++)
                {
                    PictureItemControl pictureItem = new PictureItemControl(this.pictureCanvas);
                    pictureItem.MoveFinished += PictureItem_MoveFinished;

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(paths[i]);
                    bitmapImage.EndInit();
                    pictureItem.ImageSource = bitmapImage;

                    this.pictureCanvas.Children.Add(pictureItem);
                    pictureItem.HorizontalAlignment = HorizontalAlignment.Left;
                    pictureItem.VerticalAlignment = VerticalAlignment.Top;
                    _8i = i * 8;
                    pictureItem.Margin = new Thickness(mousePosition.X + _8i, mousePosition.Y + _8i, 0, 0);

#warning HERE
                    //this.Pic ADD
                }
            }
            catch (Exception ex)
            {
                this.EndInit();
                throw ex;
            }

            this.EndInit();
        }
        private void PictureItem_MoveFinished(object sender, EventArgs e)
        {
#warning HERE
            //var sameName = from val in this.pictureSetProjectFile.PictureItems
            //    where val.Name==
        }

        private void button_AddPicture_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_SetBasePath_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
