using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class ParticipantInfoConfiguration : IEntityTypeConfiguration<ParticipantInfo>
    {
        public void Configure(EntityTypeBuilder<ParticipantInfo> builder)
        {
            builder.ToTable("ParticipantInfos");
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<ParticipantInfo>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasPrincipalKey<User>(x => x.Id);

            
        }
    }
}
