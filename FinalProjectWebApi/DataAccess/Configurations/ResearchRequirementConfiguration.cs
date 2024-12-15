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

            

           
        
        }
    }
}
