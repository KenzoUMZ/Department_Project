using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Shared.Model
{
    public class Infrastructure
    {
        public int InfrastructureId { get; set; } // PK
        public string OfficeLocation { get; set; }
        public int NumberOfDesks { get; set; }
        public int NumberOfComputers { get; set; }
        public bool HasMeetingRooms { get; set; }

        // Chave estrangeira e relação 1:1 com DepartmentEntity
        public int DepartmentId { get; set; } // FK
        public virtual DepartmentEntity Department { get; set; }

        // Removendo o parâmetro de navegação do construtor
        public Infrastructure(string officeLocation, int numberOfDesks, int numberOfComputers, bool hasMeetingRooms)
        {
            OfficeLocation = officeLocation;
            NumberOfDesks = numberOfDesks;
            NumberOfComputers = numberOfComputers;
            HasMeetingRooms = hasMeetingRooms;
        }
    }
}
