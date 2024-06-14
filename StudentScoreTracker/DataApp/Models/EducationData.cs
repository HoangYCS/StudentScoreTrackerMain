using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataApp.Models
{
    public class EducationData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        public string SBD { get; set; }

        [StringLength(250)]
        [Required]
        public string MaHS { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal DiemToan { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal DiemVan { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal DiemAnh { get; set; }

        [StringLength(250)]
        [Required]
        public string ThongTinDiem { get; set; }

        public int CurrentYear { get; set; }

        public virtual List<SpecializedScore> SpecializedScores { get; set; } = new List<SpecializedScore>();

        public virtual List<DualDegreeScore> DualDegreeScores { get; set; } = new List<DualDegreeScore>();

        [NotMapped]
        public decimal TongDiem => (DiemToan + DiemVan) * 2 + DiemAnh;

        public EducationData(string sBD, string thongTinDiem, string maHS, int currentYear)
        {
            SBD = sBD;
            MaHS = maHS;
            CurrentYear = currentYear;
            ThongTinDiem = thongTinDiem;
            ParseThongTinDiem(thongTinDiem);
        }

        private void ParseThongTinDiem(string thongTinDiem)
        {
            DiemVan = ExtractScore(thongTinDiem, "Ngữ văn");
            DiemAnh = ExtractScore(thongTinDiem, "Ngoại ngữ");
            DiemToan = ExtractScore(thongTinDiem, "Toán");
            ExtractSpecializedScores(thongTinDiem);
            ExtractDualDegreeScores(thongTinDiem);
        }

        private decimal ExtractScore(string input, string subject)
        {
            string pattern = $"{subject}:\\s*([\\d.]+)";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                return decimal.Parse(match.Groups[1].Value);
            }
            else
            {
                return 0;
            }
        }

        private void ExtractSpecializedScores(string input)
        {
            string pattern = "Chuyên\\s*(\\d+):\\s*([\\d.]+)";
            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    string subjectName = $"Chuyên {match.Groups[1].Value}";
                    decimal score = decimal.TryParse(match.Groups[2].Value, out var parsedScore) ? parsedScore : 0;

                    SpecializedScores.Add(new SpecializedScore
                    {
                        Id = Guid.NewGuid().ToString(),
                        SubjectName = subjectName,
                        Score = score,
                        EducationDataKey = SBD
                    });
                }
            }
        }

        private void ExtractDualDegreeScores(string input)
        {
            string pattern = "Điểm song bằng v2:\\s*([^;]+)";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string[] subjects = match.Groups[1].Value.Split(',');

                foreach (var subject in subjects)
                {
                    string[] parts = subject.Trim().Split(':');
                    if (parts.Length == 2)
                    {
                        string subjectName = parts[0].Trim();
                        decimal score = decimal.TryParse(parts[1].Trim(), out var parsedScore) ? parsedScore : 0;

                        DualDegreeScores.Add(new DualDegreeScore
                        {
                            Id = Guid.NewGuid().ToString(),
                            SubjectName = subjectName,
                            Score = score,
                            EducationDataKey = SBD
                        });
                    }
                }
            }
        }

        public EducationData() { }

        public EducationData(string sBD, string maHS, decimal diemToan, decimal diemVan, decimal diemAnh, string thongTinDiem, int currentYear, List<SpecializedScore> specializedScores, List<DualDegreeScore> dualDegreeScores)
        {
            SBD = sBD;
            MaHS = maHS;
            DiemToan = diemToan;
            DiemVan = diemVan;
            DiemAnh = diemAnh;
            ThongTinDiem = thongTinDiem;
            CurrentYear = currentYear;
            SpecializedScores = specializedScores;
            DualDegreeScores = dualDegreeScores;
        }
    }

}
