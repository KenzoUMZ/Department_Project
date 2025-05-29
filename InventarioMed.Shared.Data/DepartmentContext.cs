using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Department.Shared.Data;
using Department.Shared.Data.Models;
using Department.Shared.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Department.Shared.Data
{
    public class DepartmentContext : IdentityDbContext<AccessUser, AccessRole, int>
    {
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DepartmentDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString)
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Definir chave composta
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            // Chave estrangeira para Employee
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany()  // Se Employee não tem um ICollection<EmployeeProject>
                .HasForeignKey(ep => ep.EmployeeId);

            // Chave estrangeira para Project
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects) // Project tem ICollection<EmployeeProject>
                .HasForeignKey(ep => ep.ProjectId);

            modelBuilder.Entity<DepartmentEntity>()
            .HasOne(d => d.Infrastructure)  // Um Departamento tem uma Infraestrutura
            .WithOne(i => i.Department)     // Uma Infraestrutura pertence a um único Departamento
            .HasForeignKey<Infrastructure>(i => i.DepartmentId); // Define a chave estrangeira
        }

    }
}