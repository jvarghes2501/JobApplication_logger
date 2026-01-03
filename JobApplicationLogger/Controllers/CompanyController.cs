using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index(string searchBy, string? searchString, string sortBy = nameof(CompanyResponse.Name),
            SortOrderOptions sortOrder = SortOrderOptions.Ascending)
        {
            List<CompanyResponse> companies = await _companyService.GetFilteredCompanies(searchBy, searchString);
            ViewBag.currentSearchBy = searchBy;
            ViewBag.currentSearchString = searchString;

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(CompanyResponse.Name), "Name" },
                {nameof(CompanyResponse.PositionName), "Position Name" },
                {nameof(CompanyResponse.Website), "Website" },
                {nameof(CompanyResponse.isCoverLetter), "Is Cover Letter" },
                {nameof(CompanyResponse.CreatedAt), "Created at" }

            };

            List<CompanyResponse> sortedCompanies = await _companyService.GetSortedCompanies(companies, sortBy, sortOrder);
            ViewBag.currentSortBy = sortBy;
            ViewBag.currentSortOrder = sortOrder.ToString();
            return View(sortedCompanies);//views/company/Index.cshtml
        }

        [Route("[action]")]
        [HttpGet]

        public async Task <IActionResult> Add()
        {
            
            List<CompanyResponse> allCompanies = await _companyService.GetAllCompanies();
            ViewBag.companies = allCompanies.Select(
                temp => new SelectListItem()
                {
                    Text = temp.Name,
                    Value = temp.CompanyID.ToString(),
                }); 
            return View();//views/company/Add.cshtml
        }

        [Route("[action]")]
        [HttpPost]

        public async Task<IActionResult> Add(CompanyAddRequest companyAddRequest)
        {
            if(!ModelState.IsValid)
            {
                List<CompanyResponse> allCompanies = await _companyService.GetAllCompanies();
                ViewBag.companies = allCompanies.Select(
                    temp => new SelectListItem()
                    {
                        Text = temp.Name,
                        Value = temp.CompanyID.ToString(),
                    });
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
            CompanyResponse newCompany = await _companyService.AddCompany(companyAddRequest);
            return RedirectToAction("index", "company");
        }

        [HttpGet]
        [Route("[action]/{companyId}")]
        public async Task<IActionResult> Edit(Guid companyID)
        {
            CompanyResponse? companyResponse = await _companyService.GetCompanyByCompanyId(companyID);
            if (companyResponse == null)
            {
                return RedirectToAction("index");
            }
            CompanyUpdateRequest companyUpdateReq = companyResponse.ToCompanyUpdateRequest();
            List<CompanyResponse> allCompanies = await _companyService.GetAllCompanies();
            ViewBag.companies = allCompanies.Select(
                temp => new SelectListItem()
                {
                    Text = temp.Name,
                    Value = temp.CompanyID.ToString(),
                });
            return View(companyUpdateReq);
        }

        [HttpPost]
        [Route("[action]/{companyId}")]
        public async Task<IActionResult> Edit(CompanyUpdateRequest companyUpdateRequest)
        {
            CompanyResponse? companyResponse = await _companyService.GetCompanyByCompanyId(companyUpdateRequest.CompanyID);
            if (companyResponse == null)
            {
                return RedirectToAction("index");
            }
            if (ModelState.IsValid)
            {
                CompanyResponse updatedCompany = await _companyService.UpdateCompany(companyUpdateRequest);
                return RedirectToAction("index");
            }
            else
            {
                List<CompanyResponse> companies = await _companyService.GetAllCompanies();
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
        public async Task<IActionResult> Delete(Guid? companyID)
        {
            CompanyResponse? companyResponse = await _companyService.GetCompanyByCompanyId(companyID);
            if (companyResponse == null)
            {
                return RedirectToAction("index");
            }
            return View(companyResponse);
        }

        [HttpPost]
        [Route("[action]/{companyId}")]
        public async Task<IActionResult> Delete(CompanyUpdateRequest companyUpdateRequest)
        {
            CompanyResponse? companyResponse = await _companyService.GetCompanyByCompanyId(companyUpdateRequest.CompanyID);
            if ( companyResponse == null)
            {
                return RedirectToAction("index"); 
            }
            await _companyService.DeleteCompanyByCompanyId(companyUpdateRequest.CompanyID);
            return RedirectToAction("index");
        }
    }
}

