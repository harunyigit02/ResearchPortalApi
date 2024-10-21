using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class ViewsConfiguration : IEntityTypeConfiguration<Views>
    {
        public void Configure(EntityTypeBuilder<Views> builder)
        {
            builder.ToTable("Views");
            builder.HasKey(x => x.Id);


            builder.HasOne(v => v.Article)
                   .WithMany()
                   .HasForeignKey(v => v.ViewedArticle)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasPrincipalKey(v => v.Id);
        }
    }
}
