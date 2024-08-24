using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using irs.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using irs.API.DueDiligence.Domain.Model;
using irs.API.IAM.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace irs.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Vendor>().HasKey(v => v.Id);

        builder.Entity<Vendor>().Property(v => v.BusinessName).IsRequired();
        builder.Entity<Vendor>().Property(v => v.TradeName).IsRequired();
        builder.Entity<Vendor>().Property(v => v.TaxId).IsRequired().HasMaxLength(11);
        builder.Entity<Vendor>().Property(v => v.PhoneNumber).IsRequired();
        builder.Entity<Vendor>().Property(v => v.Email).IsRequired();
        builder.Entity<Vendor>().Property(v => v.Website).IsRequired();
        builder.Entity<Vendor>().Property(v => v.Address).IsRequired();
        builder.Entity<Vendor>().Property(v => v.Country).IsRequired();
        builder.Entity<Vendor>().Property(v => v.AnnualBilling).IsRequired().HasColumnType("decimal(18,2)");

        
        // IAM Context
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}