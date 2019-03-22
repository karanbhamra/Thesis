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
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Name</th>
                                        <th>LastName</th>
                                        <th>Age</th>
                                        <th>Gender</th>
                                    </tr>");


                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", student.FirstName, student.MiddleName, student.LastName, student.DateOfBirth.ToShortDateString());

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}