using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class EthnicityConfiguration:IEntityTypeConfiguration<Ethnicity>
    {
        public void Configure(EntityTypeBuilder<Ethnicity> builder)
        {
            builder.ToTable("Ethnicities");
            builder.HasKey(x => x.Id);
            
        }
    }
}
