using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Research)
                  .WithMany(q=>q.Questions)
                  .HasForeignKey(a => a.ResearchId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasPrincipalKey(x => x.Id);

        }
    }
}
