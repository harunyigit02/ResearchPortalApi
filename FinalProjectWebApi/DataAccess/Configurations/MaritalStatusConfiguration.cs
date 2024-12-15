using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class MaritalStatusConfiguration : IEntityTypeConfiguration<MaritalStatus>
    {
        public void Configure(EntityTypeBuilder<MaritalStatus> builder)
        {
            builder.ToTable("MaritalStatuses");
            builder.HasKey(x => x.Id);
            
        }
    }
}
