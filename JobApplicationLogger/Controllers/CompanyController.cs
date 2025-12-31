using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace JobApplicationLogger.Controllers
{
    [Route("[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [Route("[action]")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(CompanyResponse.Name),
            SortOrderOptions sortOrder = SortOrderOptions.Ascending)
        {
            List<CompanyResponse> companies = _companyService.GetFilteredCompanies(searchBy, searchString);

            ViewBag.currentSearchBy = searchBy;
            ViewBag.currentSearchString = searchString;

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(CompanyResponse.Name), "Name" },
                {nameof(CompanyResponse.PositionName), "Position Name" },
                {nameof(CompanyResponse.Website), "Website" },
                {nameof(CompanyResponse.isCoverLetter), "Is Cover Letter" }

            };

            List<CompanyResponse> sortedCompanies = _companyService.GetSortedCompanies(companies, sortBy, sortOrder);
            ViewBag.currentSortBy = sortBy;
            ViewBag.currentSortOrder = sortOrder.ToString();
            return View(sortedCompanies);//views/company/Index.cshtml
        }

        [Route("[action]")]
        [HttpGet]

        public IActionResult Add()
        {
            return View();//views/company/Add.cshtml
        }

        [Route("[action]")]
        [HttpPost]

        public IActionResult Add(CompanyAddRequest companyAddRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
            CompanyResponse companyResponse = _companyService.AddCompany(companyAddRequest);
            return RedirectToAction("Index", "Company");
        }

        [HttpGet]
        [Route("[action]/{companyId}")]
        public IActionResult Edit(Guid companyID)
        {
            CompanyResponse? companyResponse = _companyService.GetCompanyByCompanyId(companyID);
            if (companyResponse == null)
            {
                return RedirectToAction("index");
            }
            CompanyUpdateRequest companyUpdateReq = companyResponse.ToCompanyUpdateRequest();
            List<CompanyResponse> allCompanies = _companyService.GetAllCompanies();
            ViewBag.companies = allCompanies.Select(
                temp => new SelectListItem()
                {
                    Text = temp.Name,
                    Value = temp.CompanyID.ToString(),
                }).ToList();
            return View(companyUpdateReq);
        }

        [HttpPost]
        [Route("[action]/{companyId}")]
        public IActionResult Edit(CompanyUpdateRequest companyUpdateRequest)
        {
            CompanyResponse? companyResponse = _companyService.GetCompanyByCompanyId(companyUpdateRequest.CompanyID);
            if (companyResponse == null)
            {
                return RedirectToAction("index");
            }
            if (ModelState.IsValid)
            {
                CompanyResponse updatedCompany = _companyService.UpdateCompany(companyUpdateRequest);
                return RedirectToAction("index");
            }
            else
            {
                List<CompanyResponse> companies = _companyService.GetAllCompanies();
                ViewBag.companies = companies.Select(
                    temp => new SelectListItem()
                    {
                        Text = temp.Name,
                        Value = temp.CompanyID.ToString(),
                    });
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(companyResponse.ToCompanyUpdateRequest());
            }
        }

        [HttpGet]
        [Route("[action]/{companyId}")]
        public IActionResult Delete(Guid? companyID)
        {
            CompanyResponse? companyResponse = _companyService.GetCompanyByCompanyId(companyID);
            if (companyResponse == null)
            {
                return RedirectToAction("index");
            }
            return View(companyResponse);
        }

        [HttpPost]
        [Route("[action]/{companyId}")]
        public IActionResult Delete(CompanyUpdateRequest companyUpdateRequest)
        {
            CompanyResponse? companyResponse = _companyService.GetCompanyByCompanyId(companyUpdateRequest.CompanyID);
            if ( companyResponse == null)
            {
                return RedirectToAction("index"); 
            }
            _companyService.DeleteCompanyByCompanyId(companyUpdateRequest.CompanyID);
            return RedirectToAction("index");
        }
    }
}

