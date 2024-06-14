using Dapper;
using DataApp.ContextDB;
using DataApp.Models;
using Microsoft.Data.SqlClient;
using RespositoryApp.DTOs;
using RespositoryApp.IRespositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryApp.Respositories
{
    public class EducationDataRespository : IEducationDataRespository
    {
        private readonly IDbConnection _connection;
        private readonly EducationDbContext _educationDbContext;

        public EducationDataRespository(IDbConnection connection, EducationDbContext educationDbContext)
        {
            _connection = connection;
            _educationDbContext = educationDbContext;
        }

        public async  Task<EducationData?> LookUpScoreInformationBySBDOrMaHSAsync(string? SBDOrMaSV, int year)
        {
            //CREATE PROCEDURE sp_LookUpScoreInformationBySBDOrMaHS
            //                @SBDOrMaSV NVARCHAR(50),
            //    @CurrentYear INT
            //AS
            //BEGIN
            //    SELECT SBD, MaHS, DiemToan, DiemVan, DiemAnh, ThongTinDiem, CurrentYear
            //    FROM Educations
            //    WHERE(SBD = @SBDOrMaSV OR MaHS = @SBDOrMaSV) AND CurrentYear = @CurrentYear
            //END


            if (SBDOrMaSV == null) return null;

            var parameters = new { SBDOrMaSV = SBDOrMaSV.Trim(), CurrentYear = year };

            var educationData = await _connection.QueryFirstOrDefaultAsync<EducationData>(
                "sp_LookUpScoreInformationBySBDOrMaHS",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return educationData;
        }


        public async Task<ResponseServer> ImportEducationDataFromExcelAsync(DataExelImportDTO dataExelImpoortDTO)
        {
            try
            {
                await _educationDbContext.AddRangeAsync(dataExelImpoortDTO.EducationDatas);
                await _educationDbContext.SaveChangesAsync();
                return new ResponseServer()
                {
                    Message = $"{dataExelImpoortDTO.EducationDatas.Count} items đã được thêm vào cơ sở dữ liệu!",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ResponseServer()
                {
                    Message = "Lỗi data",
                    Success = false
                };
            }
        }

    }
}
