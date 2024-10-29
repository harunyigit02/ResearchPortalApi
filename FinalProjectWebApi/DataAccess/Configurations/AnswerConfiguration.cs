using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Options)
                .WithMany(a=>a.Answers)
                .HasForeignKey(a => a.OptionId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
