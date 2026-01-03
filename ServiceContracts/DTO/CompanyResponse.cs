using System;
using Entities;

namespace ServiceContracts.DTO
{
    public class  CompanyResponse
    {
        public Guid CompanyID { get; set;  }
        public string? Name { get; set; }

        public string? PositionName { get; set; }

        public bool isCoverLetter { get; set; } = false;
        public string? Website { get; set; }
        // Auditing
        public DateTime CreatedAt { get; set; }

        public CompanyApplicationStatus Status { get; set; } = CompanyApplicationStatus.Unknown;

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
                && this.CreatedAt == company_to_check.CreatedAt
                && this.Status == company_to_check.Status);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"CompanyID: {CompanyID}, Name: {Name}, PositionName: {PositionName}, isCoverLetter: {isCoverLetter}, Website: {Website}, CreatedAt: {CreatedAt}, Status:{Status}";
        }

        public CompanyUpdateRequest ToCompanyUpdateRequest()
        {
            return new CompanyUpdateRequest()
            {
                CompanyID = this.CompanyID,
                Name = this.Name,
                Website = this.Website,
                positionName = this.PositionName,
                isCoverLetter = this.isCoverLetter,
                CreatedAt = this.CreatedAt, 
                Status = this.Status
            };
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
                CreatedAt = company.CreatedAt, 
                Status = company.Status
            };
        }
    }
}
