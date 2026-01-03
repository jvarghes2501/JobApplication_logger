using ServiceContracts.DTO;
using ServiceContracts.Enums;


namespace ServiceContracts
{
    public interface ICompanyService
    {
        /*
         * Adds a new company based on the provided CompanyAddRequest object to the data store.
         */
        Task <CompanyResponse> AddCompany(CompanyAddRequest? companyAddRequest);
        
        /*
         * Retrieves all companies from the data store and returns them as a list of CompanyResponse objects.
         */
        Task<List<CompanyResponse>> GetAllCompanies();

        /*
         * Retrieves a specific company by its unique identifier (companyId) from the data store.
         */
        Task<CompanyResponse?> GetCompanyByCompanyId(Guid? companyId);

        /*
         * Retrieves companies from the data store based on filtering criteria.
         * The filtering is done based on the searchBy parameter (e.g., "Name", "Address") 
         * and the searchString parameter which contains the value to filter by.
         */
        Task<List<CompanyResponse>> GetFilteredCompanies(string searchBy, string? searchString);

        /*
         * Retrieves companies from the data store sorted based on the sortBy parameter 
         * (e.g., "Name", "Address") and the sortOrder parameter which indicates ascending or descending order.
         */
        Task<List<CompanyResponse>> GetSortedCompanies(List<CompanyResponse> allCompanies, string sortBy, SortOrderOptions sortOrder);

        /*
         * Updates an existing company in the data store based on the provided CompanyUpdateRequest object.
         */
        Task<CompanyResponse> UpdateCompany(CompanyUpdateRequest? companyUpdateRequest);

        /*
         * Deletes a specific company by its unique identifier (companyId) from the data store.
         */
        Task<bool> DeleteCompanyByCompanyId(Guid? companyId);
    }
}
