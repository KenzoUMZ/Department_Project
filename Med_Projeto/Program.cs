using Department.Shared.Data;
using Department.Shared.Model;
using InventarioMed.Shared.Data;

internal class Program
{
    public static Dictionary<string, DepartmentEntity> DepartmentList = new();

    private static void Main(string[] args)
    {
        var DepartmentDAL = new DepartmentDAL();

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Department!\n");
            Console.WriteLine("Press 1 to register a department");
            Console.WriteLine("Press 2 to register an employee in a department");
            Console.WriteLine("Press 3 to show all departments");
            Console.WriteLine("Press 4 to show employees of a department");
            Console.WriteLine("Press -1 to exit\n");

            Console.WriteLine("Choose your option:");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    DepartmentRegistration();
                    break;
                case 2:
                    EmployeeRegistration();
                    break;
                case 3:
                    DepartmentGet();
                    break;
                case 4:
                    EmployeeGet();
                    break;
                case -1:
                    Console.WriteLine("Goodbye!");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        void DepartmentRegistration()
        {
            Console.Clear();
            Console.WriteLine("Department Registration");
            Console.WriteLine("Enter the name of the department:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the location of the department:");
            string location = Console.ReadLine();
            DepartmentEntity d = new(name, location);
            DepartmentDAL.Create(d);
            Console.WriteLine($"Department {name} added successfully!");
            Console.ReadKey();
        }

        void DepartmentGet()
        {
            Console.Clear();
            Console.WriteLine("List of Departments:");
            foreach (var department in DepartmentDAL.Read())
            {
                Console.WriteLine(department);
            }
            Console.ReadKey();
        }

        void EmployeeGet()
        {
            Console.Clear();
            Console.WriteLine("Show employees in a department");
            Console.WriteLine("Enter the department name:");
            string departmentName = Console.ReadLine();
            var targetDepartment = DepartmentDAL.ReadBy(dep => dep.Name.Equals(departmentName));
            if (targetDepartment is not null)
            {
                targetDepartment.ShowEmployees();
            }
            else
            {
                Console.WriteLine($"Department {departmentName} does not exist.");
                return;
            }

            if (DepartmentList.ContainsKey(departmentName))
            {
                DepartmentEntity department = DepartmentList[departmentName];
                department.ShowEmployees();
            }
            else
            {
                Console.WriteLine($"Department {departmentName} does not exist.");
            }
        }

        void EmployeeRegistration()
        {
            Console.Clear();
            Console.WriteLine("Employee Registration");
            Console.WriteLine("Enter the department where the employee will be registered:");
            string departmentName = Console.ReadLine();
            var targetDepartment = DepartmentDAL.ReadBy(dep => dep.Name.Equals(departmentName));
            if (targetDepartment != null)
            {
                DepartmentList.Add(departmentName, targetDepartment);
            }
            else
            {
                Console.WriteLine($"Department {departmentName} does not exist.");
                return;
            }

            if (DepartmentList.ContainsKey(departmentName))
            {
                Console.WriteLine($"Enter the employee name for {departmentName}:");
                string name = Console.ReadLine();
                Console.WriteLine($"Enter the employee position:");
                string position = Console.ReadLine();
                DepartmentEntity department = DepartmentList[departmentName];
                Employee emp = new(name, position, 0, department);
                DepartmentEntity d = DepartmentList[departmentName];
                d.AddEmployee(emp);
                Console.WriteLine($"Employee {name} added to {departmentName} successfully!");
            }
            else
            {
                Console.WriteLine($"Department {departmentName} does not exist.");
            }
        }
    }
}