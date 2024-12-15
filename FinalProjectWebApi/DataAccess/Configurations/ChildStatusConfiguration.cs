using FinalProjectWebApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectWebApi.DataAccess.Configurations
{
    public class ChildStatusConfiguration : IEntityTypeConfiguration<ChildStatus>
    {
        public void Configure(EntityTypeBuilder<ChildStatus> builder)
        {
            builder.ToTable("ChildStatuses");
            builder.HasKey(x => x.Id);
            
        }
    }
}
