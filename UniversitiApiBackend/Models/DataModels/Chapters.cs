using System.ComponentModel.DataAnnotations;

namespace UniversitiApiBackend.Models.DataModels
{
    public class Chapters : BaseEntity
    {
        // relacion de uno a uno con el curso
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = new Course();
        [Required]
        public string List = string.Empty;
    }
}
