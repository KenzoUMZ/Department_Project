using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Department.Shared.Model;

namespace Department.Shared.Data
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public string? Role { get; set; } // Employee's role in the project
        public int HoursAllocated { get; set; } // Hours assigned to the project
    }
}
