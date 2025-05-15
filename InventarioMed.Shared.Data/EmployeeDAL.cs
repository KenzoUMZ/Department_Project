using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Department.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Department.Shared.Data
{
    public class EmployeeDAL
    {
        private readonly DepartmentContext _context;

        public EmployeeDAL()
        {
            _context = new DepartmentContext();
        }

        public void Create(Employee employee)
        {
            if (employee == null)
            {
                Console.WriteLine("Invalid employee data: Employee is null.");
                return;
            }

            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public IEnumerable<Employee> Read()
        {
            return _context.Employees.ToList();
        }

        public Employee? ReadById(int id)
        {
            return _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
        }

        public void Update(Employee employee)
        {
            if (employee == null)
            {
                Console.WriteLine("Invalid employee data: Employee is null.");
                return;
            }

            var existingEmployee = _context.Employees.Find(employee.EmployeeId);
            if (existingEmployee == null)
            {
                Console.WriteLine($"Employee with ID {employee.EmployeeId} does not exist.");
                return;
            }

            if (employee != null)
            {
                _context.Employees.Update(employee);
            }
            _context.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            if (employee == null)
            {
                Console.WriteLine("Invalid employee data: Employee is null.");
                return;
            }

            var existingEmployee = _context.Employees.Find(employee.EmployeeId);
            if (existingEmployee == null)
            {
                Console.WriteLine($"Employee with ID {employee.EmployeeId} does not exist.");
                return;
            }

            _context.Employees.Remove(existingEmployee);
            _context.SaveChanges();
        }
    }
}