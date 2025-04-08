using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyDataAccess.DTOs;

namespace SurveyDataAccess.Configurations
{
    public class QuestionGroupConfiguration : IEntityTypeConfiguration<QuestionGroupDTO>
    {
        public void Configure(EntityTypeBuilder<QuestionGroupDTO> builder)
        {
            builder.ToTable("Table_QuestionGroups");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.NameEN).HasColumnType("nvarchar(255)");
            builder.Property(s => s.NameVN).HasColumnType("nvarchar(255)");
            builder.Property(s => s.Description).HasColumnType("nvarchar(500)");
            builder.Property(s => s.Priority).HasColumnType("tinyint");
            builder.Property(s => s.IsActive).HasDefaultValue(true);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedBy).HasColumnType("varchar(100)");
            builder.Property(s => s.ModifiedBy).HasColumnType("varchar(100)");
        }
    }
}
