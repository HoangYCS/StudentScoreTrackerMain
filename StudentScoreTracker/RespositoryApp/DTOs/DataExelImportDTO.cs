using DataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryApp.DTOs
{
    public class DataExelImportDTO
    {
        public List<EducationData> EducationDatas { get; set; }  = new List<EducationData>();
    }
}
