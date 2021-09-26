using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteCheck.Domain;
using System;

namespace SiteCheck.Persistence.Configurations
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SiteLink).IsRequired();
        }
    }
}
