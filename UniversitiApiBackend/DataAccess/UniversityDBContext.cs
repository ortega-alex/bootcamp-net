using Microsoft.EntityFrameworkCore;
using UniversitiApiBackend.Models.DataModels;

namespace UniversitiApiBackend.DataAccess
{
    public class UniversityDBContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options, ILoggerFactory loggerFactory) : base(options)  
        {
            _loggerFactory = loggerFactory;
        }

        // TODO: Add DbSet (Tables of our Data base)
        public DbSet<User>? Users { get; set; } // Table of Users
        public DbSet<Course>? Course { get; set; }
        public DbSet<Chapters>? Indexes { get; set; } 
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Student>? Students { get; set; }


        // cada que existe un cambio en el modelo quedara persistida
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger = _loggerFactory.CreateLogger<UniversityDBContext>();
            //optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name }));
            //optionsBuilder.EnableSensitiveDataLogging(); 
                       
            optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Information)  // filtar datos que sea de nivel informativo
                .EnableSensitiveDataLogging() // para que guarde los datos sensibles en la base de datos
                .EnableDetailedErrors(); // si hay error que sean lo mas detallados posibles
        }
    }
}
