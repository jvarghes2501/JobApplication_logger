using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Company
    {
        [Key]
        public Guid CompanyID { get; set; } = Guid.NewGuid();
        
        [StringLength(200)]
        public string? Name { get; set; }

        [StringLength(200)]
        public string? PositionName { get; set; }
        
        [StringLength(500)]
        public string? Website { get; set; }

        
        public bool isCoverLetter { get; set; } = false;


        // Auditing
        public DateTime CreatedAt { get; set; }

        public CompanyApplicationStatus Status { get; set; } = CompanyApplicationStatus.Unknown;


    }
}