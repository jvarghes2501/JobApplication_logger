using Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class CompanyUpdateRequest
    {
        [Required(ErrorMessage = "CompanyID is required")]
        public Guid CompanyID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "positionName is required")]
        public string? positionName { get; set; }

        public bool isCoverLetter { get; set; } = false;

        public string? Website { get; set; }
        public DateTime CreatedAt { get; set; }

        public CompanyApplicationStatus Status { get; set; } = CompanyApplicationStatus.Unknown;




        public Company toCompany()
        {
            return new Company()
            {
                CompanyID = this.CompanyID,
                Name = this.Name,
                PositionName = this.positionName,
                isCoverLetter = this.isCoverLetter,
                Website = this.Website,
                CreatedAt = this.CreatedAt, 
                Status = this.Status,
            };
        }
    }
}
