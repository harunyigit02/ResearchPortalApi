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

        }
    }
}
