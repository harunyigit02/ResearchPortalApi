using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class ParentalStatusConfiguration : IEntityTypeConfiguration<ParentalStatus>
    {
        public void Configure(EntityTypeBuilder<ParentalStatus> builder)
        {
            builder.ToTable("ParentalStatuses");
            builder.HasKey(x => x.Id);
            
        }
    }
}
