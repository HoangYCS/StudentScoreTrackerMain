using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApp.Models
{
    public class DualDegreeScore
    {
        [MaxLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(250)]
        public string SubjectName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal Score { get; set; }

        [ForeignKey("EducationData")]
        [MaxLength(50)]
        public string EducationDataKey { get; set; }

        public virtual EducationData? EducationData { get; set; }
    }
}
