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
    //ASI, Auto Set Init.
    //
    //错误：不能在同一实例上具有嵌套的 BeginInit 调用。
    //所以把ASI去了。
    //可能实际上有一部代码也不需要Init状态，即ASI多余。
    public partial class PictureItemControl
    {
        #region ==Name==
        //private string _PI_Name = null;
        ///// <remarks>
        ///// 默认为null。
        ///// </remarks>
        //public string PI_Name
        //{
        //    get
        //    {
        //        return _PI_Name;
        //    }
        //    set
        //    {
        //        _PI_Name = value;
        //    }
        //}
        #endregion

        #region ==Position==
        public System.Windows.Point PI_Position
        {
            get
            {
                return new System.Windows.Point(Canvas.GetLeft(this) - this.RenderTransformOrigin.X, Canvas.GetTop(this) - this.RenderTransformOrigin.Y);
            }
            set
            {
                Canvas.SetLeft(this, value.X + this.RenderTransformOrigin.X);
                Canvas.SetTop(this, value.Y + this.RenderTransformOrigin.Y);
            }
        }
        private void Refresh_PI_Position()
        {
            PI_Position = PI_Position;
        }
        //public System.Windows.Point PI_Position_ASI
        //{
        //    set
        //    {
        //        this.BeginInit();
        //        try
        //        {
        //            Canvas.SetLeft(this, value.X + this.RenderTransformOrigin.X);
        //            Canvas.SetTop(this, value.Y + this.RenderTransformOrigin.Y);
        //        }
        //        catch (Exception ex)
        //        {
        //            this.EndInit();
        //            throw ex;
        //        }
        //        this.EndInit();
        //    }
        //}
        //private void Refresh_PI_Position_ASI()
        //{
        //    PI_Position_ASI = PI_Position_ASI;
        //}
        #endregion

        #region ==TransformOrigin==
        public System.Windows.Point PI_TransformOrigin
        {
            get
            {
                return this.RenderTransformOrigin;
            }
            set
            {
                this.RenderTransformOrigin = value;
                this.MainGrid_RotateTransform.CenterX = value.X;
                this.MainGrid_RotateTransform.CenterY = value.Y;

                Refresh_PI_Position();
#warning 这样做操作的刷新渲染结果可能有点反人类？
                //Refresh_PI_RotateAngle();
            }
        }
        //public System.Windows.Point PI_TransformOrigin_ASI
        //{
        //    set
        //    {
        //        this.BeginInit();
        //        try
        //        {
        //            this.RenderTransformOrigin = value;
        //            this.MainGrid_RotateTransform.CenterX = value.X;
        //            this.MainGrid_RotateTransform.CenterY = value.Y;
        //        }
        //        catch (Exception ex)
        //        {
        //            this.EndInit();
        //            throw ex;
        //        }
        //        this.EndInit();
        //
        //        Refresh_PI_Position_ASI();
        //#warning 这样做操作的刷新渲染结果可能有点反人类？
        //        //Refresh_PI_RotateAngle_ASI();
        //    }
        //}
        #endregion

        #region ==Scale==
        //Source的高度是原图像高度
        public System.Windows.Point PI_Scale
        {
            get
            {
                return new System.Windows.Point(
                    this.Width / this.Image_Picture.Source.Width,
                    this.Height / this.Image_Picture.Source.Height);
            }
            set
            {
                this.Width = this.Image_Picture.Source.Width * value.X;
                this.Height = this.Image_Picture.Source.Height * value.Y;
            }
        }
        //public System.Windows.Point PI_Scale_ASI
        //{
        //    set
        //    {
        //        this.BeginInit();
        //        try
        //        {
        //            this.Width = this.Image_Picture.Source.Width * value.X;
        //            this.Height = this.Image_Picture.Source.Height * value.Y;
        //        }
        //        catch (Exception ex)
        //        {
        //            this.EndInit();
        //            throw ex;
        //        }
        //        this.EndInit();
        //    }
        //}
        #endregion

        #region ==RotateAngle==
        public double PI_RotateAngle
        {
            get
            {
                return MainGrid_RotateTransform.Angle;
            }
            set
            {
                this.MainGrid_RotateTransform.Angle = value;
            }
        }
        //private void Refresh_PI_RotateAngle()
        //{
        //    PI_RotateAngle = PI_RotateAngle;
        //}
        //public double PI_RotateAngle_ASI
        //{
        //    set
        //    {
        //        this.BeginInit();
        //        try
        //        {
        //            this.MainGrid_RotateTransform.Angle = value;
        //        }
        //        catch (Exception ex)
        //        {
        //            this.EndInit();
        //            throw ex;
        //        }
        //        this.EndInit();
        //    }
        //}
        ////private void Refresh_PI_RotateAngle_ASI()
        ////{
        ////    PI_RotateAngle_ASI = PI_RotateAngle_ASI;
        ////}
        #endregion

        #region ==Opacity==
        public double PI_Opacity
        {
            get
            {
                return this.Image_Picture.Opacity;
            }
            set
            {
                this.Image_Picture.Opacity = value;
            }
        }
        //public double PI_Opacity_ASI
        //{
        //    set
        //    {
        //        this.BeginInit();
        //        try
        //        {
        //            this.Image_Picture.Opacity = value;
        //        }
        //        catch (Exception ex)
        //        {
        //            this.EndInit();
        //            throw ex;
        //        }
        //        this.EndInit();
        //    }
        //}
        #endregion

        #region ==ZIndex==
        public int PI_ZIndex
        {
            get
            {
                //return Panel.GetZIndex(this.Image_Picture);
                return Panel.GetZIndex(this);
            }
            set
            {
                //Panel.SetZIndex(this.Image_Picture, value);
                Panel.SetZIndex(this, value);
                //pictureItemContainer.Container_Canvas.UpdateLayout();
            }
        }
        //public int PI_ZIndex_ASI
        //{
        //    set
        //    {
        //        this.BeginInit();
        //        try
        //        {
        //            Panel.SetZIndex(this.Image_Picture, value);
        //        }
        //        catch (Exception ex)
        //        {
        //            this.EndInit();
        //            throw ex;
        //        }
        //        this.EndInit();
        //    }
        //}
        #endregion

        #region ==Path==
        //即ImageSource。
        public string PI_Path
        {
            get
            {
                return this.ImageSource.ToString();
            }
            set
            {
                this.ImageSource = new BitmapImage(new Uri(value));
            }
        }
        //public string PI_Path_ASI
        //{
        //    set
        //    {
        //        this.BeginInit();
        //        try
        //        {
        //            this.ImageSource = new BitmapImage(new Uri(value));
        //        }
        //        catch (Exception ex)
        //        {
        //            this.EndInit();
        //            throw ex;
        //        }
        //        this.EndInit();
        //    }
        //}
        #endregion

        #region ==ExValues==
        //private Dictionary<string, string> _PI_ExValues = null;
        ///// <remarks>
        ///// 默认为null。
        ///// </remarks>
        //public Dictionary<string, string> PI_ExValues
        //{
        //    get
        //    {
        //        return _PI_ExValues;
        //    }
        //    set
        //    {
        //        _PI_ExValues = value;
        //    }
        //}
        #endregion

        public static PictureItemControl Create(IPictureItemContainer pictureItemContainer, PictureItemStructure data)
        {
#pragma warning disable IDE0017 // 简化对象初始化
            PictureItemControl r = new PictureItemControl(pictureItemContainer);
#pragma warning restore IDE0017 // 简化对象初始化
#warning 必须先初始化Path
            r.PI_Path = data.Path;
            r.PI_Position = ToPoint(data.Position);
            r.PI_TransformOrigin = ToPoint(data.TransformOrigin);
            r.PI_Scale = ToPoint(data.Scale);
            r.PI_RotateAngle = data.RotateAngle;
            r.PI_Opacity = data.Opacity;
            r.PI_ZIndex = data.ZIndex;

            return r;
        }
        public static System.Windows.Point ToPoint(Tuple<double, double> tuple)
        {
            return new System.Windows.Point(tuple.Item1, tuple.Item2);
        }

        public PictureItemStructure ToPictureItemStructure()
        {
            return new PictureItemStructure()
            {
                Name = null,
                Position = ToTuple(this.PI_Position),
                TransformOrigin = ToTuple(this.PI_TransformOrigin),
                Scale = ToTuple(this.PI_Scale),
                RotateAngle = this.PI_RotateAngle,
                Opacity = this.PI_Opacity,
                ZIndex = this.PI_ZIndex,
                Path = this.PI_Path,
                ExValues = null
            };
        }
        public static Tuple<double, double> ToTuple(System.Windows.Point point)
        {
            return new Tuple<double, double>(point.X, point.Y);
        }
    }
}
