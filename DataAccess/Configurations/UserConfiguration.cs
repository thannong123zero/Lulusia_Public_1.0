using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDTO>
    {
        public void Configure(EntityTypeBuilder<UserDTO> builder)
        {
            builder.ToTable("TBSystem_Users");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.CreatedOn).IsRequired();
            builder.Property(s => s.ModifiedOn).IsRequired();
            builder.Property(s => s.Email).IsRequired();
            builder.HasIndex(s => s.Email).IsUnique();
        }
    }
}
