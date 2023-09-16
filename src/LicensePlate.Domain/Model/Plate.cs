using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicensePlate.Domain
{
    public class Plate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(4)]
        public string Chars { get; set; }
        
        [Required]
        public int Numbers { get; set; }
        
        public bool IsActive { get; set; } = false;
        
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedDate { get; set; }
    }
}
