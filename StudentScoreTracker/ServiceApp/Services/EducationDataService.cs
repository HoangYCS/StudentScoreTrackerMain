using Azure;
using DataApp.Models;
using RespositoryApp.DTOs;
using RespositoryApp.IRespositories;
using ServiceApp.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Services
{
    public class EducationDataService : IEducationDataService
    {
        private readonly IEducationDataRespository _educationDataRespository;

        public EducationDataService(IEducationDataRespository educationDataRespository)
        {
            _educationDataRespository = educationDataRespository;
        }

        public async Task<ResponseServer> ImportEducationDataFromExcelAsync(DataExelImportDTO dataExelImportDTO)
        {
            return await _educationDataRespository.ImportEducationDataFromExcelAsync(dataExelImportDTO);
        }

        public async Task<EducationData?> LookUpScoreInformationBySBDOrMaHSAsync(string? SBDOrMaSV, int year)
        {
           return await _educationDataRespository.LookUpScoreInformationBySBDOrMaHSAsync(SBDOrMaSV, year);
        }
    }
}
