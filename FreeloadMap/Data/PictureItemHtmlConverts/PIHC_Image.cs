using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AngleSharp;

using FreeloadMap.Lib.Data;
using FreeloadMap.Lib.Data.PictureItemTypes;
using FreeloadMap.Lib.Utility;

namespace FreeloadMap.Data.PictureItemHtmlConverts
{
    public class PIHC_Image : IPictureItemHtmlConvert
    {
        #region ==IPictureItemHtmlConvert==
        public string Name { get { return "Image"; } }

        public Type InputType { get { return typeof(PIT_Image); } }

        public string GetStyle(object obj)
        {
            return GetStyle((PIT_Image)obj);
        }

        public void Initialize(object obj, string style)
        {
            this.pit_image = (PIT_Image)obj;
            this.style = style;
        }

        public void Work(IEnumerable<IPictureItemData> loadedData)
        {
            ;
        }

        public string GetHtml()
        {
            return GetHtml(pit_image);
        }

        private string GetHtml(PIT_Image image)
        {
            string style = this.style != null ? this.style : GetStyle(image);

            string elementText =  ;
        }

        private string GetStyle(PIT_Image image)
        {
#if _SYS_PATH_URI
            Bitmap bitmap = (Bitmap)Bitmap.FromFile(System.IO.Path.Combine("wwwroot", PictureItem.Path));
#elif _LIB_PATH_URI
            Bitmap bitmap = (Bitmap)Bitmap.FromFile(FkPath.Combine("wwwroot", pit_image.Path, FkPath.DirectorySeparator.Backslash));
#endif

#warning 不清楚中心点是否与scale有关
            StringBuilder styleText = new StringBuilder();
            styleText.Append(String.Format(
                "transform-origin: {0}% {1}%; " +
                //"transform: scale({2}, {3}); " +
                "width: {2}px; " + //注意：这里貌似用百分比不行
                "height: {3}px; " + //注意：这里貌似用百分比不行
                "transform: rotate({4}deg); " +
                "opacity: {5}; " +
                "position: absolute;",
                pit_image.TransformOrigin.Item1 / bitmap.Width * 100.0,
                pit_image.TransformOrigin.Item2 / bitmap.Height * 100.0,
                //pit_image.Scale.Item1,
                //pit_image.Scale.Item2,
                pit_image.Scale.Item1 * bitmap.Width/* * (useShadowClass || ForceShadow ? 1.2 : 1.0)*/,
                pit_image.Scale.Item2 * bitmap.Height/* * (useShadowClass || ForceShadow ? 1.2 : 1.0)*/,
                pit_image.RotateAngle,
                pit_image.Opacity
            ));
#warning String.Format会忽略掉第7个及以后的参数？？？

            return styleText.ToString();
        }
        #endregion

        private PIT_Image pit_image;
        private string style;
    }
}
