using Attendance_Management_System.Models;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace Attendance_Management_System.DataAccess
{
    public class MyDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> AttendanceRecords { get; set; }
        public DbSet<User> Users { get; set; } // Add DbSet for the Users table

        public MyDbContext() : base(Properties.Settings.Default.DbConnect)
        {
        }

        public Company GetCompanyById(int companyId)
        {
            return Companies.FirstOrDefault(c => c.CompanyId == companyId);
        }

        public void InsertCompany(Company company)
        {
            Companies.Add(company);
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
