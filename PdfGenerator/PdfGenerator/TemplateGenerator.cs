using System.Collections.Generic;
using System.Text;

namespace PdfGenerator
{
    public class Employee
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
    
    public class TemplateGenerator
    {
        static List<Employee> employees = new List<Employee>
        {
            new Employee { Name="Mike", LastName="Turner", Age=35, Gender="Male"},
            new Employee { Name="Sonja", LastName="Markus", Age=22, Gender="Female"},
            new Employee { Name="Luck", LastName="Martins", Age=40, Gender="Male"},
            new Employee { Name="Sofia", LastName="Packner", Age=30, Gender="Female"},
            new Employee { Name="John", LastName="Doe", Age=45, Gender="Male"}
        };
        
        
        
        
        public  static string GetHTMLString()
        {
            //var employees = DataStorage.GetAllEmployess();
 
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
 
            foreach (var emp in employees)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", emp.Name, emp.LastName, emp.Age, emp.Gender);
            }
 
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
 
            return sb.ToString();
        }
    }
}