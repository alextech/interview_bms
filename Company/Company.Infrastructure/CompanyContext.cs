using Company.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel;

namespace Company.Infrastructure;

public class CompanyContext : DbContext, IUnitOfWork
{
    public DbSet<Domain.Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // =============== COMPANY ================

        // unfortunate namespace name collision. Maybe better entity name?
        EntityTypeBuilder<Domain.Company> companyEntity = modelBuilder
            .Entity<Domain.Company>();

        // ============== USER ==================
        EntityTypeBuilder<User> userEntity = modelBuilder
            .Entity<User>();
        userEntity.Property<int>("Id")
            .ValueGeneratedOnAdd();
        userEntity.HasKey("Id");

        userEntity
            .HasIndex(u => u.Email)
            .IsUnique();

    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}