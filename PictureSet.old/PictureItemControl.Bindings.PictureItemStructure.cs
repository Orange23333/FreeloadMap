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
    public partial class PictureItemControl
    {
        public Binding Binding_Name { get; set; }

        public Binding Binding_Position { get; set; }

        public Binding Binding_TransformOrigin { get; set; }

        public Binding Binding_Scale { get; set; }

        public Binding Binding_RotateAngle { get; set; }

        public Binding Binding_Opacity { get; set; }

        public Binding Binding_ZIndex { get; set; }

        public Binding Binding_Path { get; set; }

        public Binding Binding_ExValue { get; set; }

        public void Init_Bindings_PictureItemStructure()
        {
            Binding_Name = new Binding()
            {
                Source = pictureItemStructure.Name,
                Path = new PropertyPath("")
            };

        }
    }
}
