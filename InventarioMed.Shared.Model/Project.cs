using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Shared.Model
{
    public class Project

    {
        public int Id { get; set; } // Primary key
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Updated: Establishing a proper many-to-many relationship with Employee
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public Project(string title, string description, DateTime startDate, DateTime endDate)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

}
