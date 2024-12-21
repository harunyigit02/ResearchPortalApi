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
                   .WithMany()  // Research, Requirement'ları takip etmez
                   .HasForeignKey(rr => rr.ResearchId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasPrincipalKey(r => r.Id);

            builder.Property(r => r.Ethnicity)
                   .HasColumnType("integer[]");
            builder.Property(r => r.Gender)
                   .HasColumnType("integer[]");
            builder.Property(r => r.EducationLevel)
                   .HasColumnType("integer[]");
            builder.Property(r => r.HousingType)
                   .HasColumnType("integer[]");
            builder.Property(r => r.ChildStatus)
                   .HasColumnType("integer[]");
            builder.Property(r => r.ParentalStatus)
                   .HasColumnType("integer[]");
            builder.Property(r => r.DisabilityStatus)
                   .HasColumnType("integer[]");
            builder.Property(r => r.MaritalStatus)
                   .HasColumnType("integer[]");
            builder.Property(r => r.Occupation)
                   .HasColumnType("integer[]");









        }
    }
}
