using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VOCDataAccess.DTOs;

namespace VOCDataAccess.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<DepartmentDTO>
    {
        public void Configure(EntityTypeBuilder<DepartmentDTO> builder)
        {
            builder.ToTable("Table_Departments");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.Name).HasColumnType("nvarchar(255)");
            builder.Property(s => s.Description).HasColumnType("nvarchar(500)");
            builder.Property(s => s.CreatedOn).IsRequired();
            builder.Property(s => s.ModifiedOn).IsRequired();
            builder.Property(s => s.IsActive).HasDefaultValue(true);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.Priority).HasColumnType("tinyint");
            builder.Property(s => s.CreatedBy).HasColumnType("varchar(100)");
            builder.Property(s => s.ModifiedBy).HasColumnType("varchar(100)");
        }
    }
}
