using DataApp.Models;
using RespositoryApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.IServices
{
    public interface IEducationDataService
    {
        Task<ResponseServer> ImportEducationDataFromExcelAsync(DataExelImportDTO dataExelImpoortDTO);
        Task<EducationData?> LookUpScoreInformationBySBDOrMaHSAsync(string? SBDOrMaSV, int year);
    }
}
