using System;
using System.Collections.Generic;
using System.Text;
using StudentRecordTool.Models;

namespace PdfGenerator
{
    
    public class TemplateGenerator
    {
        public static string GetHTMLString(BasicStudent student)
        {
            string fullname = $"{student.FirstName} {student.MiddleName} {student.LastName}";

            string output = $"<style>#header{{text-align:center}}h1{{color:red}} .title{{color:black}}</style><div id=\"header\"><h1>California State University, Northridge</h1><h1 class=\"title\">DegreeVerify Certificate</h1><hr><h4>Date Requested:</h4> {DateTime.Now}<hr></div><div id=mainBody><h3>Information Verified</h3><br><br><p><h5>Name on School's Records:</h5>{fullname} <br><h5>Date Awarded:</h5> {student.Awarded.ToShortDateString()}<br><h5>Degree Title:</h5> {student.Degree}<br><h5>Name of School:</h5> {student.Organization}<br><h5>School Division:</h5> {student.SchoolDivision} <br><h5>Major Course(s) of Study:</h5> {student.Major}<br></div><hr>";

            return output;
        }
    }
}