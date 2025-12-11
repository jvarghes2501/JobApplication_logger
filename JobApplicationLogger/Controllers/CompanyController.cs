using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

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
        public IActionResult Index()
        {
            List<CompanyResponse> companies = _companyService.GetAllCompanies();
            return View(companies);//views/company/Index.cshtml
        }
    }
}

