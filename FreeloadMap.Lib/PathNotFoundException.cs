using System;
using System.IO;
using System.Runtime.Serialization;

namespace FreeloadMap.Lib
{
    public class PathNotFoundException : IOException
    {
        public PathNotFoundException() : base() {; }
#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        public PathNotFoundException(string? message) : base(message) {; }
        public PathNotFoundException(string? message, Exception? innerException) : base(message, innerException) {; }
#pragma warning restore CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        protected PathNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {; }
    }
}
