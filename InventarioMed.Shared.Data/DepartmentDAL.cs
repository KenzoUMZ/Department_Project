using System;
using System.Collections.Generic;
using System.Linq;
using Department.Shared.Model;

namespace Department.Shared.Data
{
    public class DepartmentDAL
    {
        private readonly DepartmentContext _context;

        public DepartmentDAL()
        {
            _context = new DepartmentContext();
        }

        public void Create(DepartmentEntity department)
        {
            if (department == null)
            {
                Console.WriteLine("Invalid department data: DepartmentEntity is null.");
                return;
            }

            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public IEnumerable<DepartmentEntity> Read()
        {
            return _context.Departments.ToList();
        }

        public DepartmentEntity? ReadById(int id)
        {
            return _context.Departments.FirstOrDefault(x => x.Id == id);
        }

        public DepartmentEntity? ReadBy(Func<DepartmentEntity, bool> predicate)
        {
            return _context.Departments.FirstOrDefault(predicate);
        }

        public void Update(DepartmentEntity department)
        {
            if (department == null)
            {
                Console.WriteLine("Invalid department data: DepartmentEntity is null.");
                return;
            }

            var existingDepartment = _context.Departments.Find(department.Id);
            if (existingDepartment == null)
            {
                Console.WriteLine($"DepartmentEntity with ID {department.Id} does not exist.");
                return;
            }

            _context.Departments.Update(department);
            _context.SaveChanges();
        }

        public void Delete(DepartmentEntity department)
        {
            if (department == null)
            {
                Console.WriteLine("Invalid department data: DepartmentEntity is null.");
                return;
            }

            var existingDepartment = _context.Departments.Find(department.Id);
            if (existingDepartment == null)
            {
                Console.WriteLine($"DepartmentEntity with ID {department.Id} does not exist.");
                return;
            }

            _context.Departments.Remove(existingDepartment);
            _context.SaveChanges();
        }
    }
}
