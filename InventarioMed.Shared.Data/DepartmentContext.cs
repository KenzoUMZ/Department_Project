using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Department.Shared.Data;
using Department.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Department.Shared.Data
{
    public class DepartmentContext : DbContext
    {
        private const string ConnectionString = "Server=tcp:departmentserver.database.windows.net,1433;Initial Catalog=Department_DB_V0;Persist Security Info=False;User ID=abacaxi_adm;Password=h@ytaee5Np3AF#5;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite primary key for EmployeeProject
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            // ... any other configuration ...
        }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}