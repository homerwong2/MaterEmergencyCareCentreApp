using MaterEmergencyCareCentreApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MaterEmergencyCareCentreApp.DataAccess
{
    public class BedApiContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "BedDb");
            optionsBuilder.EnableSensitiveDataLogging();

        }

        public DbSet<Bed> Beds { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        // public DbSet<Employee> Employees { get; set; }
    }
}