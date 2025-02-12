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
            builder.HasOne(x => x.Option)
                .WithMany(a=>a.Answers)
                .HasForeignKey(a => a.OptionId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(a => a.QuestionId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(a => a.ParticipantId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
