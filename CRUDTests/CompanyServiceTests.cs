//using Xunit;
//using ServiceContracts;
//using Xunit.Abstractions;
//using ServiceContracts.DTO;
//namespace CRUDTests
//{
//    public class CompanyServiceTests
//    {
//        private readonly ICompanyService _companyService;
//        public CompanyServiceTests()
//        {
//            // Initialize the CompanyService before each test
//            _companyService = new Services.CompanyService(false);
//        }

//        #region AddCompany
//        [Fact]
//        public void AddCompany_NullCompany()
//        {
//            CompanyAddRequest? companyAddRequest = null;
//            Assert.Throws<ArgumentNullException>(() => _companyService.AddCompany(companyAddRequest));
//        }

//        [Fact]
//        public void AddCompany_CompanyNameNull()
//        {
//            CompanyAddRequest? companyAddRequest = new CompanyAddRequest() { Name = null};
//            Assert.Throws<ArgumentException>(() => _companyService.AddCompany(companyAddRequest));
//        }

//        [Fact]
//        public void AddCompany_CorrectCompanyInfo()
//        {
//            CompanyAddRequest? companyAddRequest = new CompanyAddRequest() { 
//                Name="Dofasco", 
//                Website="dofasco.com"};

//            CompanyResponse companyResponse = _companyService.AddCompany(companyAddRequest);

//            List<CompanyResponse> allCompanies = _companyService.GetAllCompanies();
//            Assert.True(companyResponse.CompanyID != Guid.Empty);
//            Assert.Contains(companyResponse,allCompanies );
//        }
//        #endregion

//        #region GetCompanyByCompanyId
//        [Fact]
//        public void GetCompanybyID_nullID()
//        {
//            Guid ?companyId = null;
//            CompanyResponse? companyResponse = _companyService.GetCompanyByCompanyId(companyId);
//            Assert.Null(companyResponse);
//        }

//        [Fact]
//        public void GetCompanybyID_correctID()
//        {
//            CompanyAddRequest? companyAddRequest = new CompanyAddRequest()
//            {
//                Name = "Dofasco",
//                Website = "dofasco.com"
//            };
//            CompanyResponse companyResponse = _companyService.AddCompany(companyAddRequest);
//            CompanyResponse? fetchedCompany = _companyService.GetCompanyByCompanyId(companyResponse.CompanyID);
//            Assert.Equal(companyResponse, fetchedCompany);
//        }

//        #endregion

//        #region getAllCompanies

//        [Fact]
//        public void GetAllCompanies_EmptyDataStore()
//        {
//            List<CompanyResponse> allCompanies = _companyService.GetAllCompanies();
//            Assert.Empty(allCompanies);
//        }

//        [Fact]
//        public void GetAllCompanies_NonEmptyDataStore()
//        {
//            CompanyAddRequest? companyAddRequest1 = new CompanyAddRequest()
//            {
//                Name = "Dofasco",
//                Website = "dofasco.com"
//            };
//            CompanyAddRequest? companyAddRequest2 = new CompanyAddRequest()
//            {
//                Name = "honda",
//                Website = "honda.com"
//            };
//            CompanyResponse companyResponse1 = _companyService.AddCompany(companyAddRequest1);
//            CompanyResponse companyResponse2 = _companyService.AddCompany(companyAddRequest2);
//            List<CompanyResponse> allCompanies = _companyService.GetAllCompanies();
//            Assert.Equal(2, allCompanies.Count);
//            Assert.Contains(companyResponse1, allCompanies);
//            Assert.Contains(companyResponse2, allCompanies);
//        }
//        #endregion

//        #region GetFilteredCompanies
//        [Fact]
//        public void GetFilteredCompanies_emptySearchString()
//        {
//            CompanyAddRequest? companyAddRequest1 = new CompanyAddRequest()
//            {
//                Name = "Dofasco",
//                Website = "dofasco.com"
//            };
//            CompanyAddRequest? companyAddRequest2 = new CompanyAddRequest()
//            {
//                Name = "honda",
//                Website = "honda.com"
//            };
//            CompanyResponse companyResponse1 = _companyService.AddCompany(companyAddRequest1);
//            CompanyResponse companyResponse2 = _companyService.AddCompany(companyAddRequest2);
//            List<CompanyResponse> filteredCompanies = _companyService.GetFilteredCompanies("Name", null);
//            Assert.Equal(2, filteredCompanies.Count);
//            Assert.Contains(companyResponse1, filteredCompanies);
//            Assert.Contains(companyResponse2, filteredCompanies);
//        }

//        [Fact]
//        public void GetFilteredCompanies_CorrectSearchString()
//        {
//            CompanyAddRequest? companyAddRequest1 = new CompanyAddRequest()
//            {
//                Name = "Dofasco",
//                Website = "dofasco.com"
//            };
//            CompanyAddRequest? companyAddRequest2 = new CompanyAddRequest()
//            {
//                Name = "honda",
//                Website = "honda.com"
//            };
//            CompanyResponse companyResponse1 = _companyService.AddCompany(companyAddRequest1);
//            CompanyResponse companyResponse2 = _companyService.AddCompany(companyAddRequest2);
//            List<CompanyResponse> filteredCompanies = _companyService.GetFilteredCompanies("Name", "do");
//            Assert.Single(filteredCompanies);
//            Assert.Contains(companyResponse1, filteredCompanies);
//        }

//        #endregion

//        #region GetSortedCompanies
//        [Fact]
//        public void GetSortedCompanies_SortByName_Ascending()
//        {
//            CompanyAddRequest? companyAddRequest1 = new CompanyAddRequest()
//            {
//                Name = "Dofasco",
//                Website = "dofasco.com"
//            };
//            CompanyAddRequest? companyAddRequest2 = new CompanyAddRequest()
//            {
//                Name = "Honda",
//                Website = "honda.com"
//            };
//            CompanyResponse companyResponse1 = _companyService.AddCompany(companyAddRequest1);
//            CompanyResponse companyResponse2 = _companyService.AddCompany(companyAddRequest2);
//            List<CompanyResponse> allCompanies = _companyService.GetAllCompanies();
//            List<CompanyResponse> sortedCompanies = _companyService.GetSortedCompanies(allCompanies, "Name", ServiceContracts.Enums.SortOrderOptions.Ascending);
//            Assert.Equal(2, sortedCompanies.Count);
//            Assert.Equal(companyResponse1, sortedCompanies[0]);
//            Assert.Equal(companyResponse2, sortedCompanies[1]);
//        }

//        [Fact]
//        public void GetSortedCompanies_SortByName_Descending()
//        {
//            CompanyAddRequest? companyAddRequest1 = new CompanyAddRequest()
//            {
//                Name = "Dofasco",
//                Website = "dofasco.com"
//            };
//            CompanyAddRequest? companyAddRequest2 = new CompanyAddRequest()
//            {
//                Name = "Honda",
//                Website = "honda.com"
//            };
//            CompanyResponse companyResponse1 = _companyService.AddCompany(companyAddRequest1);
//            CompanyResponse companyResponse2 = _companyService.AddCompany(companyAddRequest2);
//            List<CompanyResponse> allCompanies = _companyService.GetAllCompanies();
//            List<CompanyResponse> sortedCompanies = _companyService.GetSortedCompanies(allCompanies, "Name", ServiceContracts.Enums.SortOrderOptions.Descending);
//            Assert.Equal(2, sortedCompanies.Count);
//            Assert.Equal(companyResponse2, sortedCompanies[0]);
//            Assert.Equal(companyResponse1, sortedCompanies[1]);
//        }
//        #endregion

//        #region updateCompany
//        [Fact]
//        public void UpdateCompany_NullCompany()
//        {
//            CompanyUpdateRequest? companyUpdateRequest = null;
//            Assert.Throws<ArgumentNullException>(() => _companyService.UpdateCompany(companyUpdateRequest));
//        }

//        [Fact]
//        public void UpdateCompany_CorrectCompanyInfo()
//        {
//            CompanyAddRequest? companyAddRequest = new CompanyAddRequest()
//            {
//                Name = "Dofasco",
//                Website = "dofasco.com"
//            };
//            CompanyResponse companyResponse = _companyService.AddCompany(companyAddRequest);
//            CompanyUpdateRequest? companyUpdateRequest = new CompanyUpdateRequest()
//            {
//                CompanyID = companyResponse.CompanyID,
//                Name = "Dofasco Updated",
//                Website = "dofasco-updated.com"
//            };
//            CompanyResponse updatedCompany = _companyService.UpdateCompany(companyUpdateRequest);
//            Assert.Equal(companyUpdateRequest.CompanyID, updatedCompany.CompanyID);
//            Assert.Equal(companyUpdateRequest.Name, updatedCompany.Name);
//            Assert.Equal(companyUpdateRequest.Website, updatedCompany.Website);
//        }
//        #endregion

//        #region DeleteCompanyByCompanyId
//        [Fact]
//        public void DeleteCompanyByCompanyId_NullID()
//        {
//            Guid ?companyId = null;
//            bool result = _companyService.DeleteCompanyByCompanyId(companyId);
//            Assert.False(result);
//        }

//        [Fact]
//        public void DeleteCompanyByCompanyId_CorrectID()
//        {
//            CompanyAddRequest? companyAddRequest = new CompanyAddRequest()
//            {
//                Name = "Dofasco",
//                Website = "dofasco.com"
//            };
//            CompanyResponse companyResponse = _companyService.AddCompany(companyAddRequest);
//            bool result = _companyService.DeleteCompanyByCompanyId(companyResponse.CompanyID);
//            Assert.True(result);
//            CompanyResponse? fetchedCompany = _companyService.GetCompanyByCompanyId(companyResponse.CompanyID);
//            Assert.Null(fetchedCompany);
//        }

//        #endregion
//    }


//}
