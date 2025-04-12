using System.ComponentModel.DataAnnotations;

namespace UniversitiApiBackend.Models.DataModels
{
    public class Student : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime Dob { get; set; }  // fecha de nacimiento
        public ICollection<Course> Courses { get; set; } = new List<Course>(); // cursos que tiene el estudiante
    }
}
