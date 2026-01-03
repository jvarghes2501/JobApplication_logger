using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool? isCoverLetter { get; set; }

        public string? Website { get; set; }
        public DateTime CreatedAt { get; set; }



        public Company toCompany()
        {
            return new Company()
            {
                CompanyID = this.CompanyID,
                Name = this.Name,
                PositionName = this.positionName,
                isCoverLetter = this.isCoverLetter,
                Website = this.Website,
                CreatedAt = this.CreatedAt
            };
        }
    }
}
