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
        public bool ? isCoverLetter { get; set; }
        public string? Website { get; set; }
        public Guid? LocationID { get; set; }
        DateTime CreatedAt { get; set;  } = DateTime.UtcNow;

        public Company toCompany()
        {
            return new Company()
            {
                Name = this.Name,
                Website = this.Website,
                LocationID = this.LocationID,
                CreatedAt = this.CreatedAt
            };
        }


    }
}
