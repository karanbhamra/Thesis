using StudentRecordTool.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReceiveJsonSaveToCosmosFunction
{
    class StudentMapper
    {
        public static FullStudent Map(BasicStudent basicStudent, int id = 1)
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
                RecordId = id
            };




            // set id,
            // set previous recordhash
            // set currentnodehash


            return fullStudent;
        }

        public static BasicStudent GenesisNodeStudent()
        {
            BasicStudent basicStudent = new BasicStudent
            {
                FirstName = "Test",
                MiddleName = "Test",
                LastName = "Test",
                DateOfBirth = DateTime.UnixEpoch,
                Organization = "California State University - Northridge",
                SchoolDivision = "College of Eng/ Comp Sci",
                Degree = "Bachelor of Science",
                Awarded = DateTime.UnixEpoch,
                Major = "Computer Science",
            };

            return basicStudent;
        }
    }
}
