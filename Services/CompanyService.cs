using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CompanyService : ICompanyService
    {
        private readonly companyDbContext _db;

        public CompanyService(companyDbContext companyDbContext)
        {
            _db = companyDbContext; 
        }
        public async Task<CompanyResponse> AddCompany(CompanyAddRequest? companyAddRequest)
        {
            if (companyAddRequest == null) // Validate input
            {
                throw new ArgumentNullException(nameof(companyAddRequest), "CompanyAddRequest cannot be null");
            }

            ValidationHelper.ModelValidation(companyAddRequest);

            Company newCompany = companyAddRequest.toCompany(); // Convert to Company entity
            newCompany.CompanyID = Guid.NewGuid(); // Assign a new GUID

            _db.Companies.Add(newCompany); // Add to DbContext
            await _db.SaveChangesAsync(); // Save changes asynchronously

            return newCompany.ToCompanyResponse();// Convert to CompanyResponse and return
        }

        public async Task<bool> DeleteCompanyByCompanyId(Guid? companyId)
        {
            if (companyId == null)
            {
                return false;
            }

            Company? companyToDelete = await _db.Companies.FirstOrDefaultAsync(comp => comp.CompanyID == companyId);
            if (companyToDelete == null)
            {
                return false;
            }
            _db.Companies.Remove(_db.Companies.First(comp=>comp.CompanyID == companyId));
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<CompanyResponse>> GetAllCompanies()
        {
            var companies = await _db.Companies.ToListAsync();

            return companies.Select(comp => comp.ToCompanyResponse()).ToList();
        }

        public async Task<CompanyResponse?> GetCompanyByCompanyId(Guid? companyId)
        {
           if (companyId == null)
            {
                return null;
            }
            Company?company = await _db.Companies.FirstOrDefaultAsync(comp => comp.CompanyID == companyId);
            if (company == null)
            {
                return null;
            }
            return company.ToCompanyResponse();
        }

        public async Task<List<CompanyResponse>> GetFilteredCompanies(string searchBy, string? searchString)
        {
            List <CompanyResponse> allCompanies = await GetAllCompanies();
            List<CompanyResponse> filteredCompanies = allCompanies;
            if (string.IsNullOrEmpty(searchString)||string.IsNullOrEmpty(searchBy))
            {
                return filteredCompanies;
            }

            switch (searchBy)
            {
                case nameof(CompanyResponse.Name):
                    filteredCompanies = allCompanies.Where(comp => comp.Name != null && comp.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(CompanyResponse.Website):
                    filteredCompanies = allCompanies.Where(comp => comp.Website != null && comp.Website.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(CompanyResponse.PositionName):
                    filteredCompanies = allCompanies.Where(comp => comp.PositionName != null && comp.PositionName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(CompanyResponse.CreatedAt):
                    if (DateTime.TryParse(searchString, out DateTime searchDate))
                    {
                        filteredCompanies = allCompanies.Where(comp => comp.CreatedAt.Date == searchDate.Date).ToList();
                    }
                    else
                    {
                        filteredCompanies = new List<CompanyResponse>();
                    }
                    break;

                default:
                    filteredCompanies = allCompanies;
                    break; 
            }
            return filteredCompanies;
        }

        public async Task<List<CompanyResponse>> GetSortedCompanies(List<CompanyResponse> allCompanies, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                return allCompanies;
            }

            List<CompanyResponse> sortedCompanies = (sortBy, sortOrder) switch
            {
                ("Name", SortOrderOptions.Ascending) => allCompanies.OrderBy(comp => comp.Name).ToList(),
                ("Name", SortOrderOptions.Descending) => allCompanies.OrderByDescending(comp => comp.Name).ToList(),
                ("Website", SortOrderOptions.Ascending) => allCompanies.OrderBy(comp => comp.Website).ToList(),
                ("Website", SortOrderOptions.Descending) => allCompanies.OrderByDescending(comp => comp.Website).ToList(),
                ("CreatedAt", SortOrderOptions.Ascending) => allCompanies.OrderBy(comp => comp.CreatedAt).ToList(),
                ("CreatedAt", SortOrderOptions.Descending) => allCompanies.OrderByDescending(comp => comp.CreatedAt).ToList(),
                _ => allCompanies
            };

            return sortedCompanies;
        }

        public async Task<CompanyResponse> UpdateCompany(CompanyUpdateRequest? companyUpdateRequest)
        {
            if (companyUpdateRequest == null) // Validate input
            {
                throw new ArgumentNullException(nameof(companyUpdateRequest), "CompanyUpdateRequest cannot be null");
            }

            ValidationHelper.ModelValidation(companyUpdateRequest);

            Company? existingCompany = await _db.Companies.FirstOrDefaultAsync(comp => comp.CompanyID == companyUpdateRequest.CompanyID);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with ID {companyUpdateRequest.CompanyID} does not exist", nameof(companyUpdateRequest.CompanyID));
            }

            // Update properties
            existingCompany.Name = companyUpdateRequest.Name;
            existingCompany.Website = companyUpdateRequest.Website;
            existingCompany.PositionName = companyUpdateRequest.positionName;
            existingCompany.isCoverLetter = companyUpdateRequest.isCoverLetter;
            existingCompany.Status = companyUpdateRequest.Status;

            await _db.SaveChangesAsync(); // Save changes asynchronously
            return existingCompany.ToCompanyResponse();
        }
    }
}
