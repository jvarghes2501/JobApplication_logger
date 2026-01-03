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

namespace Services
{
    public class CompanyService : ICompanyService
    {
        private readonly List<Company> _companyDataStore;

        public CompanyService(bool init = true)
        {
            _companyDataStore = new List<Company>(); // Initialize the in-memory data store
            if (init)
            {
                _companyDataStore.AddRange(new List<Company>() {
                new Company() { CompanyID = Guid.Parse("0E3A741C-381D-4B79-80E0-ED9619B48EEC"), Name = "Tech Solutions Inc.", 
                    PositionName = "Software Engineer",
                    isCoverLetter = true,
                    Website = "https://www.techsolutions.com",
                    CreatedAt = DateTime.UtcNow
                },
                new Company() { CompanyID = Guid.Parse("40237D7E-607E-495B-A094-608F1301B620"), 
                    PositionName = "Data Analyst",
                    isCoverLetter = false,
                    Name = "Innovatech Corp.", Website = "https://www.innovatech.com",
                    CreatedAt = DateTime.UtcNow
                },
                new Company() { CompanyID = Guid.Parse("5417A9BF-DD72-47EB-9653-BA4BB3BB242F"), 
                    PositionName = "Product Manager",
                    isCoverLetter = true,
                    Name = "Global Dynamics", Website = "https://www.globaldynamics.com",
                    CreatedAt = DateTime.UtcNow
                },
                new Company() { CompanyID = Guid.Parse("4702EB7E-3191-4EBC-AC28-A830C5B0DFA1"), Name = "NextGen Solutions", 
                    PositionName = "UX Designer",
                    isCoverLetter = false,
                    Website = "https://www.nextgensolutions.com",
                    CreatedAt = DateTime.UtcNow
                },
                new Company() { CompanyID = Guid.Parse("9FE92C56-09BC-4DEA-8AF6-BB4C8565616B"), Name = "FutureTech Labs", 
                    PositionName = "DevOps Engineer",
                    isCoverLetter = true,
                    Website = "https://www.futuretechlabs.com",
                    CreatedAt = DateTime.UtcNow
                }
                }); 

            }
        }
        public CompanyResponse AddCompany(CompanyAddRequest? companyAddRequest)
        {
            if (companyAddRequest == null) // Validate input
            {
                throw new ArgumentNullException(nameof(companyAddRequest), "CompanyAddRequest cannot be null");
            }

            ValidationHelper.ModelValidation(companyAddRequest);

            Company newCompany = companyAddRequest.toCompany(); // Convert to Company entity
            newCompany.CompanyID = Guid.NewGuid(); // Assign a new GUID
            _companyDataStore.Add(newCompany); // Add to the in-memory data store
            return newCompany.ToCompanyResponse();// Convert to CompanyResponse and return
        }

        public bool DeleteCompanyByCompanyId(Guid? companyId)
        {
            if (companyId == null)
            {
                return false;
            }

            Company? companyToDelete = _companyDataStore.FirstOrDefault(comp => comp.CompanyID == companyId);
            if (companyToDelete == null)
            {
                return false;
            }
            _companyDataStore.Remove(companyToDelete);
            return true;
        }

        public List<CompanyResponse> GetAllCompanies()
        {
            return _companyDataStore.Select(comp => comp.ToCompanyResponse()).ToList(); // Convert all Company entities to CompanyResponse and return
        }

        public CompanyResponse? GetCompanyByCompanyId(Guid? companyId)
        {
           if (companyId == null)
            {
                return null;
            }
            Company? company = _companyDataStore.FirstOrDefault(comp => comp.CompanyID == companyId);
            if (company == null)
            {
                return null;
            }
            return company.ToCompanyResponse();
        }

        public List<CompanyResponse> GetFilteredCompanies(string searchBy, string? searchString)
        {
            List <CompanyResponse> allCompanies = GetAllCompanies();
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
                default:
                    filteredCompanies = allCompanies;
                    break; 
            }
            return filteredCompanies;
        }

        public List<CompanyResponse> GetSortedCompanies(List<CompanyResponse> allCompanies, string sortBy, SortOrderOptions sortOrder)
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
                _ => allCompanies
            };

            return sortedCompanies;
        }

        public CompanyResponse UpdateCompany(CompanyUpdateRequest? companyUpdateRequest)
        {
            if (companyUpdateRequest == null) // Validate input
            {
                throw new ArgumentNullException(nameof(companyUpdateRequest), "CompanyUpdateRequest cannot be null");
            }

            ValidationHelper.ModelValidation(companyUpdateRequest);

            Company? existingCompany = _companyDataStore.FirstOrDefault(comp => comp.CompanyID == companyUpdateRequest.CompanyID);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with ID {companyUpdateRequest.CompanyID} does not exist", nameof(companyUpdateRequest.CompanyID));
            }

            // Update properties
            existingCompany.Name = companyUpdateRequest.Name;
            existingCompany.Website = companyUpdateRequest.Website;
            existingCompany.PositionName = companyUpdateRequest.positionName;
            existingCompany.isCoverLetter = companyUpdateRequest.isCoverLetter;
            return existingCompany.ToCompanyResponse();
        }
    }
}
