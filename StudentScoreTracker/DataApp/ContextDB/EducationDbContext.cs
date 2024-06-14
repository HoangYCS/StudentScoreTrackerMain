using DataApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApp.ContextDB
{
    public class EducationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<EducationData> EducationDatas { get; set; }
        public DbSet<SpecializedScore> SpecializedScores { get; set; }
        public DbSet<DualDegreeScore> DualDegreeScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationData>()
                .HasMany(e => e.SpecializedScores)
                .WithOne(s => s.EducationData)
                .HasForeignKey(s => s.EducationDataKey)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EducationData>()
                .HasMany(e => e.DualDegreeScores)
                .WithOne(d => d.EducationData)
                .HasForeignKey(d => d.EducationDataKey)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
