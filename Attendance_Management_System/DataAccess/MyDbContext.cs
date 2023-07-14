using Attendance_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_Management_System.DataAccess
{
    public class MyDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        // Add other DbSets for your data models

        public MyDbContext() : base()
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
