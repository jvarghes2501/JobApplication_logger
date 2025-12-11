using System;
using Entities;

namespace ServiceContracts.DTO
{
    public class  CompanyResponse
    {
        public Guid CompanyID { get; set;  }
        public string? Name { get; set; }

        public string? PositionName { get; set; }

        public bool? isCoverLetter { get; set; }
        public string? Website { get; set; }
        public Guid? LocationID { get; set; }
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
                && this.PositionName == company_to_check.PositionName
                && this.isCoverLetter == company_to_check.isCoverLetter
                && this.LocationID == company_to_check.LocationID
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
                PositionName = company.PositionName,
                isCoverLetter = company.isCoverLetter,
                LocationID = company.LocationID,
                CreatedAt = company.CreatedAt
            };
        }
    }
}
