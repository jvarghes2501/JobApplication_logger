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

        public string? Website { get; set; }
        public Guid? LocationID { get; set; }
        // Social / review links
        public string? LinkedInUrl { get; set; }
        public string? GlassdoorUrl { get; set; }
        public string? IndeedUrl { get; set; }
        public DateTime CreatedAt { get; set; }



        public Company toCompany()
        {
            return new Company()
            {
                CompanyID = this.CompanyID,
                Name = this.Name,
                Website = this.Website,
                LocationID = this.LocationID,
                LinkedInUrl = this.LinkedInUrl,
                GlassdoorUrl = this.GlassdoorUrl,
                IndeedUrl = this.IndeedUrl,
                CreatedAt = this.CreatedAt
            };
        }
    }
}
