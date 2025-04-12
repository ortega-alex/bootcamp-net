using System.ComponentModel.DataAnnotations;

namespace UniversitiApiBackend.Models.DataModels
{

    // establece todos los requisitos que todas las tablas necesitan y tiene que estar presentes en todas las tablas
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; } // Primary Key
        public string CreatedBy { get; set; } = string.Empty; // quien lo acreado
        public DateTime CreatedAt { get; set; } = DateTime.Now; // cuando lo a creado
        public string UpdatedBy { get; set; } = string.Empty; // quien lo a actualizado
        public DateTime? UpdatedAt { get; set; } // cuando lo a actualizado
        public string DeletedBy { get; set; } = string.Empty; // quien lo a eliminado
        public DateTime? DeletedAt { get; set; } // cuando lo a eliminado
        public bool IsDeleted { get; set; } = false; // si esta eliminado o no
    }
}
