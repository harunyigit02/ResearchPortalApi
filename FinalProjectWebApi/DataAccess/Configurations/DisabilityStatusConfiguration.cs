using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class DisabilityStatusConfiguration:IEntityTypeConfiguration<DisabilityStatus>
    {
        public void Configure(EntityTypeBuilder<DisabilityStatus> builder)
        {
            builder.ToTable("DisabilityStatuses");
            builder.HasKey(x => x.Id);
           
        }
    }
}
