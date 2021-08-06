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
#warning 未做：在container中的集体变换。
#warning 未做：利用集体变换消除左上角空余
    //TA, Transform All
    public partial class MainWindow
    {
        public void Init_Tranform()
        {
            this.ProSlider_Scale.Title = "View Scale";
            this.ProSlider_Scale.MinValue = 0.1;
            this.ProSlider_Scale.MaxValue = 100;
            this.ProSlider_Scale.Value = 1;
            this.ProSlider_Scale.Slider_Value.ValueChanged += (object sender, RoutedPropertyChangedEventArgs<double> e) =>
            {
                TA_Scale(new System.Windows.Point(e.NewValue, e.NewValue));
            };
        }

        public void TA_Scale(System.Windows.Point scale)
        {
            PictureCanvas_ScaleTransform.ScaleX = scale.X;
            PictureCanvas_ScaleTransform.ScaleY = scale.Y;
        }
    }
}
