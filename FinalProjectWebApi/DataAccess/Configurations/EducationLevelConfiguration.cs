using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class EducationLevelConfiguration:IEntityTypeConfiguration<EducationLevel>
    {
        public void Configure(EntityTypeBuilder<EducationLevel> builder)
        {
            builder.ToTable("EducationLevels");
            builder.HasKey(x => x.Id);
            
        }
    }
}
