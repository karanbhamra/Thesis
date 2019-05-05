using StudentRecordTool.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReceiveJsonSaveToCosmosFunction
{
    class StudentMapper
    {
        // This variable is needed because Azure Functions use .NetCore 2.1+ and DateTime.UnixEpoch is not backported to .NetFramework
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static FullStudent Map(BasicStudent basicStudent, string prevRecordHash, string currentNodeHash, string salt, int id = 1)
        {
            FullStudent fullStudent = new FullStudent
            {
                FirstName = basicStudent.FirstName,
                MiddleName = basicStudent.MiddleName,
                LastName = basicStudent.LastName,
                DateOfBirth = basicStudent.DateOfBirth,
                Organization = basicStudent.Organization,
                SchoolDivision = basicStudent.SchoolDivision,
                Degree = basicStudent.Degree,
                Awarded = basicStudent.Awarded,
                Major = basicStudent.Major,
                RecordId = id,
                PreviousRecordHash = prevRecordHash,
                CurrentNodeHash = currentNodeHash,
                Salt = salt
            };

            return fullStudent;
        }

        public static BasicStudent FullStudentToBasicStudent(FullStudent fullStudent)
        {
            BasicStudent basicStudent = new BasicStudent()
            {
                FirstName = fullStudent.FirstName,
                MiddleName = fullStudent.MiddleName,
                LastName = fullStudent.LastName,
                DateOfBirth = fullStudent.DateOfBirth,
                Organization = fullStudent.Organization,
                SchoolDivision = fullStudent.SchoolDivision,
                Degree = fullStudent.Degree,
                Awarded = fullStudent.Awarded,
                Major = fullStudent.Major
            };

            return basicStudent;
        }

        public static BasicStudent GenesisStudentNode()
        {
            BasicStudent basicStudent = new BasicStudent
            {
                FirstName = "Matty",
                MiddleName = "the",
                LastName = "Matador",
                DateOfBirth = UnixEpoch,//DateTime.UnixEpoch,
                Organization = "California State University - Northridge",
                SchoolDivision = "College of Eng/ Comp Sci",
                Degree = "Master of Science",
                Awarded = UnixEpoch,//DateTime.UnixEpoch,
                Major = "Computer Science",
            };

            return basicStudent;
        }
    }
}
