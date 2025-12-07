using System;
using Entities;

namespace ServiceContracts.DTO
{
    public class  CompanyResponse
    {
        public Guid CompanyID { get; set;  }
        public string? Name { get; set; }
        public string? Website { get; set; }
        public Guid? LocationID { get; set; }
        // Social / review links
        public string? LinkedInUrl { get; set; }
        public string? GlassdoorUrl { get; set; }
        public string? IndeedUrl { get; set; }
        // Auditing
        public DateTime CreatedAt { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(CompanyResponse))
            {
                return false;
            }
            CompanyResponse company_to_check = (CompanyResponse)obj;
            return (this.CompanyID == company_to_check.CompanyID
                && this.Name == company_to_check.Name
                && this.Website == company_to_check.Website
                && this.LocationID == company_to_check.LocationID
                && this.LinkedInUrl == company_to_check.LinkedInUrl
                && this.GlassdoorUrl == company_to_check.GlassdoorUrl
                && this.IndeedUrl == company_to_check.IndeedUrl
                && this.CreatedAt == company_to_check.CreatedAt);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class CompanyResponseExtensions
    {
        public static CompanyResponse ToCompanyResponse(this Company company)
        {
            return new CompanyResponse()
            {
                CompanyID = company.CompanyID,
                Name = company.Name,
                Website = company.Website,
                LocationID = company.LocationID,
                LinkedInUrl = company.LinkedInUrl,
                GlassdoorUrl = company.GlassdoorUrl,
                IndeedUrl = company.IndeedUrl,
                CreatedAt = company.CreatedAt
            };
        }
    }
}
