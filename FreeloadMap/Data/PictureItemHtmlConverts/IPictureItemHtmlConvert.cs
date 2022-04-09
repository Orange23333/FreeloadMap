using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeloadMap.Lib.Data.PictureItemTypes;

namespace FreeloadMap.Data.PictureItemHtmlConverts
{
    public interface IPictureItemHtmlConvert
    {
        string Name { get; }

        Type InputType { get; }

        /// <remarks>
        /// 应当与是否完成三步工作无关。
        /// </remarks>
        string GetStyle(object obj);


        /// <remarks>
        /// 1. 将所需要处理的对象与Html元素的style（没有应当为空字符串，而不是null）发送给转换器。
        /// </remarks>
        void Initialize(object obj, string style);

        /// <remarks>
        /// 2. 处理数据，各个数据模块之间进行交互。
        /// </remarks>
        void Work(IEnumerable<IPictureItemData> loadedData);

        /// <remarks>
        /// 3. 获取处理好的Html文本。（没有应当为空字符串，而不是null）
        /// </remarks>
        string GetHtml();
    }
}
