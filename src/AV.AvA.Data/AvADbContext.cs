using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.AvA.Model;
using Microsoft.EntityFrameworkCore;

namespace AV.AvA.Data
{
    public class AvADbContext : DbContext
    {
        public AvADbContext(DbContextOptions<AvADbContext> options)
            : base(options)
        {
        }

        public DbSet<PersonVersion> PersonVersions { get; set; } = default!;

        public DbSet<LoginToken> LoginTokens { get; set; } = default!;

        public IQueryable<int> GetCurrentPersonVersionIds() =>
            PersonVersions
                .GroupBy(pv => pv.AvId)
                .Select(g => g.Max(pv => pv.PersonVersionId));

        public IQueryable<PersonVersion> GetCurrentPersonVersions() =>
            PersonVersions
                .Where(pv => GetCurrentPersonVersionIds().Contains(pv.PersonVersionId));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonVersion>(b =>
            {
                b.HasIndex(pv => pv.AvId);
                b.Property(pv => pv.Person)
                    .HasColumnType("jsonb");
            });

            modelBuilder.Entity<LoginToken>(b =>
            {
                b.HasIndex(at => at.Token)
                    .IsUnique();
                b.HasIndex(at => at.AvId)
                    .IsUnique()
                    .HasFilter("valid_until IS NOT NULL AND used_at IS NOT NULL");
                b.Property(at => at.CreatedAt)
                    .HasDefaultValueSql("now()");
            });
        }

    }
}
