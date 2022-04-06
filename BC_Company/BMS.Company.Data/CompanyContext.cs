using System.Diagnostics.CodeAnalysis;
using BMS.Company.Domain;
using Microsoft.EntityFrameworkCore;

namespace BMS.Company.Data;

public class CompanyContext : DbContext
{
    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {

    }

    public DbSet<Domain.Company> Companies { get; set; }

    [SuppressMessage("ReSharper", "SuggestVarOrType_Elsewhere")]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // =============== COMPANY ================

        // unfortunate namespace name collision. Maybe better entity name?
        var companyEntity = modelBuilder
            .Entity<Domain.Company>();

        companyEntity.Property<int>("Id")
            .ValueGeneratedOnAdd();
        companyEntity.HasKey("Id");

        companyEntity
            .HasIndex(c => c.Guid)
            .IsUnique();

        // ============== USER ==================
        var userEntity = modelBuilder
            .Entity<User>();
        userEntity.Property<int>("Id")
            .ValueGeneratedOnAdd();
        userEntity.HasKey("Id");

        userEntity
            .HasIndex(c => c.Email)
            .IsUnique();

    }
}