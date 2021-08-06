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

namespace PictureSet
{
    public partial class MainWindow
    {
        Control leftmostControl = null;
        Control highestControl = null;
        Control rightmostControl = null;
        Control lowestControl = null;

#warning 暂时还不知道要不要通过计算来确认怎么显示滑条和布局
        /// <summary>
        /// 检查控件的位置与最控件的相比是否为新的最控件并替换。
        /// </summary>
        public void UpdateControlWheatherMost(Control control)
        {
            //Canvas.GetLeft(control);
        }
        /// <summary>
        /// 检查全部控件来比较出出最控件。
        /// </summary>
        public void UpdateControlWheatherMost(Control[] control)
        {

        }
    }
}
