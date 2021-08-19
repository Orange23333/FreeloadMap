using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StudentInfosFile
    {
        private string path = null;
        public string Path { get { return path; } set { path = value; } }

        private List<StudentInfo> studentInfos = new List<StudentInfo>();
        [JsonProperty(nameof(StudentInfos))]
        public List<StudentInfo> StudentInfos { get { return studentInfos; }set { studentInfos = value; } }

        private static readonly CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            AllowComments = true,
            Encoding = Encoding.UTF8,
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };

        //此处的random不重要，所以不必使用多个。
        private readonly static Random random = new Random();
        public int GetFreeeID()
        {
            int r = 0;
            int[] ids = (from val in studentInfos select val.ID).ToArray();

            do
            {
                r = random.Next();
            } while (ids.Contains(r));

            return r;
        }
        ////获取一个从0增长的ID
        //public IEnumerable<int> GetIncreaseIDs(int n)
        //{
        //
        //}

        public void Load(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, csvConfiguration))
            {
                this.StudentInfos = csv.GetRecords<StudentInfo>().ToList();
            }
            

            this.path = path;
        }

        /// <remarks>
        /// 不检查文件存在。
        /// </remarks>
        public void Save(string path)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(StudentInfos);
            }

            this.path = path;
        }
    }
}
