using System;

namespace Department.Shared.Model
{
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        // 🔹 Chave composta (EmployeeId + ProjectId)
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }

        // Propriedade de navegação para o Departamento
        public DepartmentEntity Department { get; set; }

        // 🔹 Construtor sem parâmetros para o EF Core
        public Employee() { }

        // 🔹 Construtor completo para uso manual (opcional)
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
