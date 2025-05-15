using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Shared.Model
{
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int EmployeeId { get; set; }

        // Updated: Establishing a proper relationship with Department (1:N)
        public DepartmentEntity Department { get; set; } // Single department per employee

        public Employee(string name, string position, int employeeId, DepartmentEntity department)
        {
            Name = name;
            Position = position;
            EmployeeId = employeeId;
            Department = department;
        }

        public override string ToString()
        {
            return $"Name: {Name} - Position: {Position} - Employee ID: {EmployeeId}";
        }
    }
}
