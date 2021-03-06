using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#warning 对于简单的单种数据文件可以直接用一个模板类完成，多数据也可以用浅/深表复制来完成。

namespace FreeloadMap.Lib.Data
{
    // 信息集合（不是不会翻译大数据（好吧，其实我的确不会，Many data？））
    public class BigInfos
    {
#warning 这里是分开的，更方便在某一类中搜索；要是是层级式的可能会更方便某种查找以及分离。
        private Dictionary<LevelLocation, string> locationPictureBindingDictionary;
        public Dictionary<LevelLocation, string> LocationPictureBindingDictionary { get { return locationPictureBindingDictionary; } }

        private Dictionary<LevelLocation, List<SchoolInfo>> locationToSchoolsDictionary;
        public Dictionary<LevelLocation, List<SchoolInfo>> LocationToSchoolsDictionary { get { return locationToSchoolsDictionary; } }
        public List<SchoolInfo> FindInLocationToSchoolsDictionary(LevelLocation key)
        {
            LevelLocation closestLocation  = LevelLocation.FindClosetLocation(LocationToSchoolsDictionary.Keys.ToArray(), key);
            return LocationToSchoolsDictionary[closestLocation];
        }

        private Dictionary<SchoolInfo, List<StudentInfo>> schoolToStudentsDictionary;
        public Dictionary<SchoolInfo, List<StudentInfo>> SchoolToStudentsDictionary { get { return schoolToStudentsDictionary; } }

#warning 支持多个文件
        public static BigInfos MakeMinimumSetByLocationPictureBindings(
            LocationPictureBindingsFile locationPictureBindingsFile,
            SchoolInfosFile schoolInfosFile,
            StudentInfosFile studentInfosFile
            )
        {
            var locations = from val in locationPictureBindingsFile.LocationPictureBindings
                            select val.Location;

            return MakeMinimumSet(
                locationPictureBindingsFile.LocationPictureBindings.ToArray(),
                locations.ToArray(),
                schoolInfosFile.SchoolInfos.ToArray(),
                studentInfosFile.StudentInfos.ToArray()
                );
        }
        public static BigInfos MakeMinimumSet(
            LocationPictureBinding[] locationPictureBindings,
            LevelLocation[] locations,
            SchoolInfo[] schools,
            StudentInfo[] students
            )
        {
            BigInfos bigInfos = new BigInfos();
            Dictionary<string, SchoolInfo> schoolNameToSchoolDictionary = new Dictionary<string, SchoolInfo>();

            foreach(LocationPictureBinding locationPictureBinding in locationPictureBindings)
            {
                bigInfos.LocationPictureBindingDictionary.Add(locationPictureBinding.Location, locationPictureBinding.PictureName);
            }

            //var includedLocations = from val in locations
            //                      where bigInfos.LocationPictureBindingDictionary.ContainsKey(val)
            //                      select val;
            var includedLocations = locations;


            List<SchoolInfo> includedSchools = new List<SchoolInfo>();
            foreach (SchoolInfo school in schools)
            {
                foreach(LevelLocation includedLocation in includedLocations)
                {
                    if (includedLocation.IsEqualsOrInclude(school.Location))
                    {
                        includedSchools.Add(school);
                        break;
                    }
                }
            }
            foreach (var includedLocation in includedLocations)
            {
                bigInfos.LocationToSchoolsDictionary.Add(includedLocation, new List<SchoolInfo>());
            }
            foreach (var includedSchool in includedSchools)
            {
                LevelLocation closestLocation = LevelLocation.FindClosetLocation(includedLocations, includedSchool.Location);
                if (closestLocation == null)
                {
                    continue;
                }

                bigInfos.LocationToSchoolsDictionary[closestLocation].Add(includedSchool);
                bigInfos.SchoolToStudentsDictionary.Add(includedSchool, new List<StudentInfo>());
                schoolNameToSchoolDictionary.Add(includedSchool.Name, includedSchool);
            }
            var includedSchoolNames = schoolNameToSchoolDictionary.Keys;
            var includedStudents = from val in students
                                   where includedSchoolNames.Contains(val.SchoolName)
                                   select val;
            foreach (var includedStudent in includedStudents)
            {
                SchoolInfo sameSchool;
                schoolNameToSchoolDictionary.TryGetValue(includedStudent.SchoolName, out sameSchool);
                if (sameSchool == null)
                {
                    continue;
                }

                // 这里sameSchool必然在字典中。
                bigInfos.SchoolToStudentsDictionary[sameSchool].Add(includedStudent);
            }

            return bigInfos;
        }

        public BigInfos FilterByLocation(LevelLocation filter)
        {
            var locationPictureBinding = from val in this.locationPictureBindingDictionary
                                         where filter.IsEqualsOrInclude(val.Key)
                                         select new LocationPictureBinding()
                                         {
                                             Location = val.Key,
                                             PictureName = val.Value
                                         };
            var locations = LocationPictureBindingDictionary.Keys;
            var selectedLocations = from val in locations
                                    where filter.IsEqualsOrInclude(val)
                                    select val;
            var students = from studentLists in SchoolToStudentsDictionary.Values
                       from val in studentLists
                       select val;

            return BigInfos.MakeMinimumSet(
                locationPictureBinding.ToArray(),
                selectedLocations.ToArray(),
                SchoolToStudentsDictionary.Keys.ToArray(),
                students.ToArray()
                );
        }

		public BigInfos()
        {
            locationPictureBindingDictionary = new Dictionary<LevelLocation, string>();
            locationToSchoolsDictionary = new Dictionary<LevelLocation, List<SchoolInfo>>();
            schoolToStudentsDictionary = new Dictionary<SchoolInfo, List<StudentInfo>>();
        }
    }
}
