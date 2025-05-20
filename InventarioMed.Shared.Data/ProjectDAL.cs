using System;
using System.Collections.Generic;
using System.Linq;
using Department.Shared.Data;
using Department.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Department.Shared.Data
{
    public class ProjectDAL
    {
        private readonly DepartmentContext context;

        public ProjectDAL()
        {
            context = new DepartmentContext();
        }

        public void Create(Project project)
        {
            if (project == null)
            {
                Console.WriteLine("Invalid project data: Project is null.");
                return;
            }

            context.Projects.Add(project);
            context.SaveChanges();
        }

        public IEnumerable<Project> Read()
        {
            return context.Projects.ToList();
        }

        public Project? ReadById(int id)
        {
            return context.Projects.FirstOrDefault(x => x.Id == id);
        }

        public Project? ReadByName(String title)
        {
            return context.Projects.FirstOrDefault(x => x.Title == title);
        }

        public void Update(Project employee)
        {
            if (employee == null)
            {
                Console.WriteLine("Invalid project data: Project is null.");
                return;
            }

            var existingEquipment = context.Projects.Find(employee.Id);
            if (existingEquipment == null)
            {
                Console.WriteLine($"Project with ID {employee.Id} does not exist.");
                return;
            }

            context.Projects.Update(employee);
            context.SaveChanges();
        }

        public void Delete(Project project)
        {
            if (project == null)
            {
                Console.WriteLine("Invalid project data: Project is null.");
                return;
            }

            var existingEquipment = context.Projects.Find(project.Id);
            if (existingEquipment == null)
            {
                Console.WriteLine($"Project with ID {project.Id} does not exist.");
                return;
            }

            context.Projects.Remove(existingEquipment);
            context.SaveChanges();
        }
    }
}
