using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace JobApplicationLogger.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [Route("/company/index")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(CompanyResponse.Name), 
            SortOrderOptions sortOrder = SortOrderOptions.Ascending)
        {
            List<CompanyResponse> companies = _companyService.GetFilteredCompanies(searchBy, searchString);

            ViewBag.currentSearchBy = searchBy;
            ViewBag.currentSearchString = searchString;

            ViewBag.SearchFields = new Dictionary <string, string>()
            {
                {nameof(CompanyResponse.Name), "Name" },
                {nameof(CompanyResponse.PositionName), "Position Name" },
                {nameof(CompanyResponse.Website), "Website" },
                {nameof(CompanyResponse.isCoverLetter), "Is Cover Letter" }
                
            };

            List<CompanyResponse>sortedCompanies = _companyService.GetSortedCompanies(companies, sortBy, sortOrder);
            ViewBag.currentSortBy = sortBy;
            ViewBag.currentSortOrder = sortOrder.ToString();
            return View(sortedCompanies);//views/company/Index.cshtml
        }
    }
}

