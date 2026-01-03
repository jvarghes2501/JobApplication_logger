using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO
{
    public class CompanyAddRequest
    {
        [Required(ErrorMessage = "CompanyName is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "postionName is required")]
        public string? positionName { get; set; }
        public bool isCoverLetter { get; set; } = false; 
        public string? Website { get; set; }
        public DateTime CreatedAt { get; set;  } = DateTime.UtcNow;

        public CompanyApplicationStatus Status { get; set; } = CompanyApplicationStatus.Unknown;

        public Company toCompany()
        {
            return new Company()
            {
                Name = this.Name,
                Website = this.Website,
                PositionName = this.positionName,
                isCoverLetter = this.isCoverLetter,
                CreatedAt = this.CreatedAt,
                Status = this.Status
            };
        }


    }
}
