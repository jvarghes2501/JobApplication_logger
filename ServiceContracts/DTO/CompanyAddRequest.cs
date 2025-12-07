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
        public string? Website { get; set; }
        public Guid? LocationID { get; set; }
        // Social / review links
        public string? LinkedInUrl { get; set; }
        public string? GlassdoorUrl { get; set; }
        public string? IndeedUrl { get; set; }
        DateTime CreatedAt { get; set;  } = DateTime.UtcNow;

        public Company toCompany()
        {
            return new Company()
            {
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
