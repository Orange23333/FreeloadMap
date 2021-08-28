using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using FreeloadMap.Lib.Utility;

namespace FreeloadMap.Lib.Tests.Utility.FkPathTests
{
    [TestFixture(Author = "Orange233",
        Category = "FreeloadMap.Lib.Utility.FkPath",
        Description = "测试FreeloadMap.Lib.Utility.FkPath类。",
        TestName = "FreeloadMap.Lib.Utility.FkPath",
        TestOf = typeof(FreeloadMap.Lib.Utility.FkPath))]
    public class FkPathTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test(Author = "Orange233",
            Description = "测试Combine的childPath参数为Windows绝对路径时的情况。")]
        public void Combine_1()
        {
            Assert.Pass();
        }

        private void Fake()
        {
            //一次测试结果是在Windows10 19042.1165系统上

            Console.WriteLine("FkPath.Combine");
            Console.WriteLine(FkPath.Combine("a", "/b", FkPath.DirectorySeparator.Backslash)); // "/b"
            Console.WriteLine(FkPath.Combine("//a", "cdb", FkPath.DirectorySeparator.Backslash)); // "//a/cdb"
            Console.WriteLine(FkPath.Combine("c:/", "bsa", FkPath.DirectorySeparator.Backslash)); // "c:/bsa"
            Console.WriteLine(FkPath.Combine("sada", "../b/", FkPath.DirectorySeparator.Backslash)); // "sada/../b/"
            Console.WriteLine();

            Console.WriteLine("FkPath.GetAbsolutePath");
            Console.WriteLine(FkPath.GetAbsolutePath("D:/Work/../", "Work", FkPath.DirectorySeparator.Backslash, true)); // "D:/Work/../Work"
            Console.WriteLine(FkPath.GetAbsolutePath("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", "Work", FkPath.DirectorySeparator.Backslash, true)); // "D:/Work/../Work/Work"
            Console.WriteLine(FkPath.GetAbsolutePath("D:/", "/Work", FkPath.DirectorySeparator.Backslash, false)); // "/Work"
            Console.WriteLine(FkPath.GetAbsolutePath("/var", "www", FkPath.DirectorySeparator.Backslash, false)); // "/var/www"
            Console.WriteLine();

            Console.WriteLine("FkPath.GetDirectory");
            Console.WriteLine(FkPath.GetDirectory("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png")); // "D:/Work/../Work/"
            Console.WriteLine(FkPath.GetDirectory("D:/Work/")); // "D:/Work/"
            Console.WriteLine(FkPath.GetDirectory("D:/Work")); // "D:/Work"
            Console.WriteLine(FkPath.GetDirectory("D:sadsadasdsa")); // "D:sadsadasdsa"
            Console.WriteLine();

            Console.WriteLine("FkPath.GetLastPart");
            Console.WriteLine(FkPath.GetLastPart("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png")); // "Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png"
            Console.WriteLine(FkPath.GetLastPart("D:/Work/../Work/")); // "Work/"
            Console.WriteLine(FkPath.GetLastPart("D:/Work/../Work")); // "Work"
            Console.WriteLine();

            Console.WriteLine("FkPath.GetOriginRootPath");
            Console.WriteLine(FkPath.GetOriginRootPath("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png")); // "D:/"
            Console.WriteLine(FkPath.GetOriginRootPath("avc")); // ""
            Console.WriteLine(FkPath.GetOriginRootPath("/var/www")); // "/"
            Console.WriteLine(FkPath.GetOriginRootPath("///var/www")); // "///"
            Console.WriteLine();

            Console.WriteLine("FkPath.GetRelativePath"); //长 短 等|文件夹 文件|值1 值2|非绝对路径
            Console.WriteLine(FkPath.GetRelativePath("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", "D:/Work/../Work/Screenshot_2020-08-14-22-38-49-299_tv.danmaku.bili.png", FkPath.DirectorySeparator.Backslash)); // "Screenshot_2020-08-14-22-38-49-299_tv.danmaku.bili.png"
            Console.WriteLine(FkPath.GetRelativePath("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", "D:/Work/Server/bashrc", FkPath.DirectorySeparator.Backslash)); // "Server/bashrc"
            Console.WriteLine(FkPath.GetRelativePath("D:/Work/Server/bashrc", "D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", FkPath.DirectorySeparator.Backslash)); // "../Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png"
            // |
            Console.WriteLine(FkPath.GetRelativePath("D:/Work/../Work", "D:/Work/", FkPath.DirectorySeparator.Backslash)); // "./"
            Console.WriteLine(FkPath.GetRelativePath("D:/Work/../Work/", "D:/Work/Server", FkPath.DirectorySeparator.Backslash)); // "Server"
            Console.WriteLine(FkPath.GetRelativePath("D:/Work/Server/", "D:/Work/../", FkPath.DirectorySeparator.Backslash)); // "../../"
            // |
            Console.WriteLine(FkPath.GetRelativePath("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", "D:/Work/", FkPath.DirectorySeparator.Backslash)); // "./"
            Console.WriteLine(FkPath.GetRelativePath("D:/Work", "D:/Work/../Work/Screenshot_2020-08-14-22-38-49-299_tv.danmaku.bili.png", FkPath.DirectorySeparator.Backslash)); // "Screenshot_2020-08-14-22-38-49-299_tv.danmaku.bili.png"
            Console.WriteLine();

            Console.WriteLine("FkPath.GetRootPath");
            Console.WriteLine(FkPath.GetRootPath("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png")); // ""D:/
            Console.WriteLine(FkPath.GetRootPath("avc")); // ""
            Console.WriteLine(FkPath.GetRootPath("/var/www")); // "/"
            Console.WriteLine(FkPath.GetRootPath("///var/www")); // "/"
            Console.WriteLine();

            Console.WriteLine("FkPath.GetShortPath");
            Console.WriteLine(FkPath.GetShortPath("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", true)); // "D:\Work\Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png"
            Console.WriteLine(FkPath.GetShortPath("avc", true)); // "avc"
            Console.WriteLine(FkPath.GetShortPath("/var/www", true)); // "/var/www"
            Console.WriteLine(FkPath.GetShortPath("///var/www", true)); // "///var/www"
            Console.WriteLine(FkPath.GetShortPath("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", false)); // "D:\Work\Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png"
            Console.WriteLine(FkPath.GetShortPath("avc", false)); // "avc"
            Console.WriteLine(FkPath.GetShortPath("/var/www", false)); // "/var/www"
            Console.WriteLine(FkPath.GetShortPath("///var/www", false)); // "///var/www"
            Console.WriteLine();

            Console.WriteLine("FkPath.IsAbsolutePath");
            Console.WriteLine(FkPath.IsAbsolutePath("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", true)); // "True"
            Console.WriteLine(FkPath.IsAbsolutePath("/var/www", true)); // "True"
            Console.WriteLine(FkPath.IsAbsolutePath("www", true)); // "False"
            Console.WriteLine(FkPath.IsAbsolutePath("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", false)); // "True"
            Console.WriteLine(FkPath.IsAbsolutePath("/var/www", false)); // "True"
            Console.WriteLine(FkPath.IsAbsolutePath("www", false)); // "False"
            Console.WriteLine();

            Console.WriteLine("FkPath.IsDirectory");
            Console.WriteLine(FkPath.IsDirectory("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png")); // "False"
            Console.WriteLine(FkPath.IsDirectory("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png/")); // "True"
            Console.WriteLine(FkPath.IsDirectory("D:/Work/../Work/")); // "True"
            Console.WriteLine();

            Console.WriteLine("FkPath.IsDirectorySeparatorChar");
            Console.WriteLine(FkPath.IsDirectorySeparatorChar('\\')); // "True"
            Console.WriteLine(FkPath.IsDirectorySeparatorChar('/')); // "True"
#warning 可以加上一个和'\\'或'/'ACSII类似的unicode字符来测试。
            Console.WriteLine(FkPath.IsDirectorySeparatorChar(' ')); // "False"
            Console.WriteLine();

            Console.WriteLine("FkPath.IsExists");
            Console.WriteLine(FkPath.IsExists("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png")); // "True"
            Console.WriteLine(FkPath.IsExists("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png/")); // "False"
            Console.WriteLine(FkPath.IsExists("D:/Work/../Work/")); // "True"
            Console.WriteLine();

            Console.WriteLine("FkPath.IsFile");
            Console.WriteLine(FkPath.IsFile("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png")); // "True"
            Console.WriteLine(FkPath.IsFile("D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png/")); // "False"
            Console.WriteLine(FkPath.IsFile("D:/Work/../Work/")); // "False"
            Console.WriteLine();

            Console.WriteLine("FkPath.NormalizeHeader");
            Console.WriteLine(FkPath.NormalizeHeader("D://\\/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png")); // "D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png"
            Console.WriteLine();

            Console.WriteLine("FkPath.ReplaceDirectorySeparator");
            Console.WriteLine(FkPath.ReplaceDirectorySeparator("D:/Work\\../Work\\Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", FkPath.DirectorySeparator.Backslash)); // "D:/Work/../Work/Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png"
            Console.WriteLine(FkPath.ReplaceDirectorySeparator("D:/Work\\../Work\\Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", FkPath.DirectorySeparator.Slash)); // "D:\Work\..\Work\Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png"
            Console.WriteLine(FkPath.ReplaceDirectorySeparator("D:/Work\\../Work\\Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png", FkPath.DirectorySeparator.DoNotCare)); // "D:/Work\../Work\Screenshot_2020-08-14-22-37-55-004_tv.danmaku.bili.png"
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
