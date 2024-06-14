using DataApp.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ServiceApp.IServices;

namespace WebApp.Controllers
{
    public class EducationManagerController : Controller
    {
        private readonly IEducationDataService _educationDataService;
        private readonly IConfiguration _config;

        public EducationManagerController(IEducationDataService educationDataService, IConfiguration config)
        {
            _educationDataService = educationDataService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportDataFromExcel(IFormFile excelfile)
        {
            var lstEducationData = new List<EducationData>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = excelfile.OpenReadStream())
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 3; row <= rowCount; row++)
                {
                    bool isRowEmpty = true;
                    for (int col = 1; col <= colCount; col++)
                    {
                        if (!string.IsNullOrWhiteSpace(worksheet.Cells[row, col].Text))
                        {
                            isRowEmpty = false;
                            break;
                        }
                    }

                    if (isRowEmpty)
                    {
                        break;
                    }

                    var year = _config.GetValue<int>("DefaultYear");
                    lstEducationData.Add(new EducationData(worksheet.Cells[row, 1].Text, worksheet.Cells[row, 3].Text, worksheet.Cells[row, 2].Text, year));
                }
            }
            return Ok(await _educationDataService.ImportEducationDataFromExcelAsync(new RespositoryApp.DTOs.DataExelImportDTO()
            {
                EducationDatas = lstEducationData
            }));
        }
    }
}
