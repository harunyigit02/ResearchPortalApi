using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class ResearchRequirementConfiguration : IEntityTypeConfiguration<ResearchRequirement>
    {
        public void Configure(EntityTypeBuilder<ResearchRequirement> builder)
        {
            
            builder.HasKey(x => x.Id);


            builder.HasOne(rr => rr.Research)
                   .WithOne()  // Research, Requirement'ları takip etmez
                   .HasForeignKey<ResearchRequirement>(x => x.ResearchId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasPrincipalKey<Research>(r => r.Id);

           builder.Property(r => r.Gender)
        .HasConversion(
            v => string.Join(',', v), // Listeyi string olarak sakla
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // String'i liste olarak yükle
        );
            builder.Property(r => r.EducationLevel)
        .HasConversion(
            v => string.Join(',', v), // Listeyi string olarak sakla
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // String'i liste olarak yükle
        );
            builder.Property(r => r.EducationLevel)
        .HasConversion(
            v => string.Join(',', v), // Listeyi string olarak sakla
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // String'i liste olarak yükle
        );
            builder.Property(r => r.Occupation)
        .HasConversion(
            v => string.Join(',', v), // Listeyi string olarak sakla
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // String'i liste olarak yükle
        );
            builder.Property(r => r.Ethnicity)
        .HasConversion(
            v => string.Join(',', v), // Listeyi string olarak sakla
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // String'i liste olarak yükle
        );
            builder.Property(r => r.MaritalStatus)
        .HasConversion(
            v => string.Join(',', v), // Listeyi string olarak sakla
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // String'i liste olarak yükle
        );
            builder.Property(r => r.ParentalStatus)
        .HasConversion(
            v => string.Join(',', v), // Listeyi string olarak sakla
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // String'i liste olarak yükle
        );
            builder.Property(r => r.ChildStatus)
        .HasConversion(
            v => string.Join(',', v), // Listeyi string olarak sakla
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // String'i liste olarak yükle
        );
            builder.Property(r => r.DisabilityStatus)
        .HasConversion(
            v => string.Join(',', v), // Listeyi string olarak sakla
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // String'i liste olarak yükle
        );
            builder.Property(r => r.HousingType)
       .HasConversion(
           v => string.Join(',', v), // Listeyi string olarak sakla
           v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // String'i liste olarak yükle
       );

        }
    }
}
