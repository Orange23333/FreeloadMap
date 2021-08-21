using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeloadMap.Data.SourceTypes
{
    public class KnownSourceType
    {
        public static readonly string Unknown = "Unknown";

        public static readonly string PictureSetProjectFile = "PictureSetProjectFile";

        // _AutoComplete_LocationPictureBinding的效用暂时与其位置无关，只要存在，在读取所有内容后从PictureSetProject中补全，这要求其中所以的PictureSet项的名字符合LevelLocation表意。
        public static readonly string _AutoComplete_LocationPictureBindings = "_AutoComplete_LocationPictureBindings";

        // _AutoComplete_LocationByLocationPictureBindings的效用暂时与其位置无关，只要存在，在读取所有内容后从LocationPictureBindings中补全。它会在_AutoComplete_LocationPictureBindings之后执行。
        public static readonly string _AutoComplete_LocationByLocationPictureBindings = "_AutoComplete_LocationByLocationPictureBindings";

        public static readonly string LocationPictureBindingsFile = "LocationPictureBindingsFile";

        public static readonly string SchoolInfosFile = "SchoolInfosFile";

        public static readonly string StudentInfosFile = "StudentInfosFile";
    }
}
