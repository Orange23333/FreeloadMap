using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using FreeloadMap.Lib.Data;

namespace PictureSet
{
#warning 一遍都调用PictureItem来设置属性，PIStructure和PIControl除了自生属性访问器不调用PictureItem，其他方法均应该尝试通过PictureItem设置。
    public class PictureItem
    {
        public Guid ID { get; set; }

        public PictureItemControl Control { get; set; }
        public PictureItemStructure Data { get; set; }

        public PictureItem(IPictureItemContainer pictureItemContainer, PictureItemStructure data) : this(Guid.Empty, pictureItemContainer, data)
        {
            ;
        }
        public PictureItem(Guid id, IPictureItemContainer pictureItemContainer, PictureItemStructure data)
        {
            this.ID = id;
            this.Data = data;
            this.Control = PictureItemControl.Create(pictureItemContainer, data);
        }

        public class PictureItem_GUIDComparer : IComparer<PictureItem>
        {
            public int Compare(PictureItem x, PictureItem y)
            {
                //if (String.Equals(x.Data.Name, y.Data.Name))
                //{
                //    return 0;
                //}
                return x.ID.CompareTo(y.ID);
            }
        }

        public class PictureItem_Comparer : IComparer<PictureItem>
        {
            public int Compare(PictureItem x, PictureItem y)
            {
                int r;

                //if (String.Equals(x.Data.Name, y.Data.Name))
                //{
                //    return 0;
                //}

                r = x.ID.CompareTo(y.ID);
                if (r != 0)
                {
                    return r;
                }

                return String.Compare(x.Data.Name, y.Data.Name);
            }
        }

        #region ==属性访问器==
        #region ==Name==
        /// <remarks>
        /// 默认为null。
        /// </remarks>
        public string Name
        {
            get
            {
                return this.Data.Name;
            }
            set
            {
                PictureItemStructure pictureItemStructure = this.Data;
                pictureItemStructure.Name = value;
                this.Data = pictureItemStructure;
            }
        }
        #endregion

        #region ==Position==
        public System.Windows.Point Position
        {
            get
            {
                System.Windows.Point point = PictureItemControl.ToPoint(this.Data.Position);
                //if (point != this.Control.PI_Position)
                //{
                //    throw new InvalidOperationException("Data's value doean't as same as Control's");
                //}
                return point;
            }
            set
            {
                PictureItemStructure pictureItemStructure = this.Data;
                pictureItemStructure.Position = PictureItemControl.ToTuple(value);
                this.Data = pictureItemStructure;

                this.Control.PI_Position = value;
            }
        }
        #endregion

        #region ==TransformOrigin==
        public System.Windows.Point TransformOrigin
        {
            get
            {
                System.Windows.Point point = PictureItemControl.ToPoint(this.Data.TransformOrigin);
                //if (point != this.Control.PI_TransformOrigin)
                //{
                //    throw new InvalidOperationException("Data's value doean't as same as Control's");
                //}
                return point;
            }
            set
            {
                PictureItemStructure pictureItemStructure = this.Data;
                pictureItemStructure.TransformOrigin = PictureItemControl.ToTuple(value);
                this.Data = pictureItemStructure;

                this.Control.PI_TransformOrigin = value;
            }
        }
        #endregion

        #region ==Scale==
        //Source的高度是原图像高度
        public System.Windows.Point Scale
        {
            get
            {
                System.Windows.Point point = PictureItemControl.ToPoint(this.Data.Scale);
                //if (point != this.Control.PI_Scale)
                //{
                //    throw new InvalidOperationException("Data's value doean't as same as Control's");
                //}
                return point;
            }
            set
            {
                PictureItemStructure pictureItemStructure = this.Data;
                pictureItemStructure.Scale = PictureItemControl.ToTuple(value);
                this.Data = pictureItemStructure;

                this.Control.PI_Scale = value;
            }
        }
        #endregion

        #region ==RotateAngle==
        public double RotateAngle
        {
            get
            {
                //if (this.Data.RotateAngle != this.Control.PI_RotateAngle)
                //{
                //    throw new InvalidOperationException("Data's value doean't as same as Control's");
                //}
                return this.Data.RotateAngle;
            }
            set
            {
                PictureItemStructure pictureItemStructure = this.Data;
                pictureItemStructure.RotateAngle = value;
                this.Data = pictureItemStructure;

                this.Control.PI_RotateAngle = value;
            }
        }
        #endregion

        #region ==Opacity==
        public double Opacity
        {
            get
            {
                //if (this.Data.Opacity != this.Control.PI_Opacity)
                //{
                //    throw new InvalidOperationException("Data's value doean't as same as Control's");
                //}
                return this.Data.Opacity;
            }
            set
            {
                PictureItemStructure pictureItemStructure = this.Data;
                pictureItemStructure.Opacity = value;
                this.Data = pictureItemStructure;

                this.Control.PI_Opacity = value;
            }
        }
        #endregion

        #region ==ZIndex==
        public int ZIndex
        {
            get
            {
                //if (this.Data.ZIndex != this.Control.PI_ZIndex)
                //{
                //    throw new InvalidOperationException("Data's value doean't as same as Control's");
                //}
                return this.Data.ZIndex;
            }
            set
            {
                PictureItemStructure pictureItemStructure = this.Data;
                pictureItemStructure.ZIndex = value;
                this.Data = pictureItemStructure;

                this.Control.PI_ZIndex = value;
            }
        }
        #endregion

#warning Name必须要及时同步为Path，检测null和空字符串。(临时)
        #region ==Path==
        public string Path
        {
            get
            {
                //if (this.Data.Path != this.Control.PI_Path)
                //{
                //    throw new InvalidOperationException("Data's value doean't as same as Control's");
                //}
                return this.Data.Path;
            }
            set
            {
                PictureItemStructure pictureItemStructure = this.Data;
                pictureItemStructure.Path = value;
                this.Data = pictureItemStructure;

                this.Control.PI_Path = value;
#warning 临时做法，暂时不考虑文件名相同的情况
                this.Name = System.IO.Path.GetFileName(value);
            }
        }
        #endregion

        #region ==ExValues==
        /// <remarks>
        /// 默认为null。
        /// </remarks>
        public Dictionary<string, string> ExValues
        {
            get
            {
                return this.Data.ExValues;
            }
            set
            {
                PictureItemStructure pictureItemStructure = this.Data;
                pictureItemStructure.ExValues = value;
                this.Data = pictureItemStructure;
            }
        }
        #endregion
        #endregion
    }
}
