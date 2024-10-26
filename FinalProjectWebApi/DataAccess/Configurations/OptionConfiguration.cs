using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {

            builder.ToTable("Options");
            builder.HasKey(x => x.Id);
            builder.HasOne(o=>o.Question)
                .WithMany(o=>o.Options)
                .HasForeignKey(o=>o.QuestionId)
                .HasPrincipalKey(o=>o.Id)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
