using StudentRecordTool.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReceiveJsonSaveToCosmosFunction
{
    class StudentMapper
    {
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




            // set id,
            // set previous recordhash
            // set currentnodehash


            return fullStudent;
        }

        public static BasicStudent GenesisStudentNode()
        {
            BasicStudent basicStudent = new BasicStudent
            {
                FirstName = "Matty",
                MiddleName = "the",
                LastName = "Matador",
                DateOfBirth = DateTime.UnixEpoch,
                Organization = "California State University - Northridge",
                SchoolDivision = "College of Eng/ Comp Sci",
                Degree = "Master of Science",
                Awarded = DateTime.UnixEpoch,
                Major = "Computer Science",
            };

            return basicStudent;
        }
    }
}
