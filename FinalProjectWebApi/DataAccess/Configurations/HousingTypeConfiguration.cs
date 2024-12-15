using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class HousingTypeConfiguration:IEntityTypeConfiguration<HousingType>
    {
        public void Configure(EntityTypeBuilder<HousingType> builder)
        {
            builder.ToTable("HousingTypes");
            builder.HasKey(x => x.Id);
            
        }
    }
}
