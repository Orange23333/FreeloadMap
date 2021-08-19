using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StudentInfo
    {
        // 用于区分相同名称和相同学校的人。
        [JsonProperty(nameof(ID))]
        [Index(0)]
        [Name("id")]
        public int ID { get; set; }

        [JsonProperty(nameof(Name))]
        [Index(1)]
        [Name("name")]
        public string Name { get; set; }

        // 应当对应SchoolInfo中的Name
        [JsonProperty(nameof(SchoolName))]
        [Index(2)]
        [Name("schoolname")]
        public string SchoolName { get; set; }

        [JsonProperty(nameof(Message))]
        [Index(3)]
        [Name("message")]
        public string Message { get; set; }

        public static IEnumerable<StudentInfo> FindByStudentName(StudentInfo[] studentInfos, string studentName)
        {
            IEnumerable<StudentInfo> r = from val in studentInfos
                                         where val.Name == studentName
                                         select val;
            return r;
        }
        public static IEnumerable<StudentInfo> FindBySchoolName(StudentInfo[] studentInfos, string schoolName)
        {
            IEnumerable<StudentInfo> r = from val in studentInfos
                                         where val.SchoolName == schoolName
                                         select val;
            return r;
        }
    }
}
