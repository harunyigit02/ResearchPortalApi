using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class ResearchConfiguration : IEntityTypeConfiguration<Research>
    {
        public void Configure(EntityTypeBuilder<Research> builder)
        {

            builder.ToTable("Researches");
            builder.HasKey(x => x.Id);


            builder.HasOne(a => a.Category)
                  .WithMany()
                  .HasForeignKey(a => a.CategoryId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasPrincipalKey(x => x.Id);

            builder.HasOne(a => a.User)
               .WithMany()
               .HasForeignKey(a => a.PublishedBy)
               .OnDelete(DeleteBehavior.SetNull)
               .HasPrincipalKey(x => x.Id);

        }
    }
}
