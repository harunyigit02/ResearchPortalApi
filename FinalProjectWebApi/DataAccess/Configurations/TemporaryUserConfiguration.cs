using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class TemporaryUserConfiguration : IEntityTypeConfiguration<TemporaryUser>
    {
        public void Configure(EntityTypeBuilder<TemporaryUser> builder)
        {
            
            builder.HasKey(x => x.Id);
            builder.ToTable("TemporaryUsers");

        }
    }
}
