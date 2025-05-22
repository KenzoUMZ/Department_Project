using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Shared.Model
{
    public class DepartmentEntity
    {
        public DepartmentEntity(String name, String location)
        {
            Name = name;
            Location = location;
        }
        public String Name { get; set; }
        public String Location { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public override String ToString()
        {
            return $@"{Id}- Nome: {Name}";
        }
        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }
        public void ShowEmployees()
        {
            Console.WriteLine($"Funcionario do Departamento {Name}");
            if (Employees.Count > 0)
            {
                foreach (Employee emp in Employees)
                {
                    Console.WriteLine(emp);
                }
            }
            else
            {
                Console.WriteLine("Nenhum funcionario cadastrado");
            }
        }
    }
}
