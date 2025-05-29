namespace Department.Shared.Model
{
    public class DepartmentEntity
    {
        public DepartmentEntity(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public string Name { get; set; }
        public string Location { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        // Relação 1:1 com Infrastructure

        public virtual Infrastructure Infrastructure { get; set; }

        public override string ToString()
        {
            return $@"{Id}- Nome: {Name}, Localização: {Location}, Infraestrutura: {(Infrastructure != null ? Infrastructure.OfficeLocation : "Não cadastrada")}";
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public void ShowEmployees()
        {
            Console.WriteLine($"Funcionários do Departamento {Name}:");
            if (Employees.Count > 0)
            {
                foreach (Employee emp in Employees)
                {
                    Console.WriteLine(emp);
                }
            }
            else
            {
                Console.WriteLine("Nenhum funcionário cadastrado.");
            }
        }

        public void ShowInfrastructure()
        {
            if (Infrastructure == null)
            {
                Console.WriteLine($"Infraestrutura do Departamento {Name} não cadastrada.");
                return;
            }

            Console.WriteLine($"Infraestrutura do Departamento {Name}:");
            Console.WriteLine($"- Local: {Infrastructure.OfficeLocation}");
            Console.WriteLine($"- Mesas: {Infrastructure.NumberOfDesks}");
            Console.WriteLine($"- Computadores: {Infrastructure.NumberOfComputers}");
            Console.WriteLine($"- Salas de reunião disponíveis: {(Infrastructure.HasMeetingRooms ? "Sim" : "Não")}");
        }
    }
}