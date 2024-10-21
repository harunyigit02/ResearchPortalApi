using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(x => x.Id);


            builder.HasOne(a => a.Category)
                  .WithMany()
                  .HasForeignKey(a => a.CategoryId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasPrincipalKey(x => x.Id);

            builder.Property(a => a.TotalViews)
                   .HasDefaultValue(0);




        }
    }
}
