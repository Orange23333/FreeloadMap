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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private SortedSet<PictureItem> pictureItems;
        //private SortedDictionary<Guid, Tuple<PictureItemStructure, PictureItemControl>> pictureItems;

        public MainWindow()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionEventHandlerMethod;

            InitializeComponent();
        }
        public void UnhandledExceptionEventHandlerMethod(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)(e.ExceptionObject);
            MessageBox.Show(exception.ToString(), "Error");
            Environment.Exit(exception.HResult);
        }

        private void ThisWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init_OpenFileDialog();
            Init_Tranform();

            pictureItems = new SortedSet<PictureItem>(new PictureItem.PictureItem_GUIDComparer());

            Init_Project();

#warning following DEBUG
            //PictureItemControl pic = new PictureItemControl(this);
            //pic.PI_Path = @"D:\Work\Project\Web\FreeloadMap\FreeloadMap\wwwroot\img\AD_CN\CN_0009_安徽省.png";
            //PictureCanvas.Children.Add(pic);
            //pic.BeginInit();
            //pic.PI_Position = new System.Windows.Point(0, 0);
            //pic.PI_Scale = new Point(1,1);
            //pic.EndInit();
        }

        private void ThisWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //目前没什么坐标变换。
        }
    }
}
