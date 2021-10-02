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

        public DbSet<LoginToken> AuthTokens { get; set; } = default!;

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
                    .HasFilter("[valid_until] IS NOT NULL AND [used_at] IS NOT NULL");
                b.Property(at => at.CreatedAt)
                    .HasDefaultValueSql("now()");
            });
        }
    }
}
