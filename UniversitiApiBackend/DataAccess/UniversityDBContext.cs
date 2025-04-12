using Microsoft.EntityFrameworkCore;
using UniversitiApiBackend.Models.DataModels;

namespace UniversitiApiBackend.DataAccess
{
    public class UniversityDBContext : DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options)
        {
        }

        // TODO: Add DbSet (Tables of our Data base)
        public DbSet<User>? Users { get; set; } // Table of Users
        public DbSet<Course>? Course { get; set; }
        public DbSet<Chapters>? Indexes { get; set; } 
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Student>? Students { get; set; } 
    }
}
