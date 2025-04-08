using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyDataAccess.DTOs;

namespace SurveyDataAccess.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<QuestionDTO>
    {
        public void Configure(EntityTypeBuilder<QuestionDTO> builder)
        {
            builder.ToTable("Table_Questions");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.QuestionGroupId).IsRequired();
            builder.Property(s => s.QuestionTypeId).IsRequired();
            builder.Property(s => s.NameEN).HasColumnType("nvarchar(255)");
            builder.Property(s => s.NameVN).HasColumnType("nvarchar(255)");
            builder.Property(s => s.ChartLabel).HasColumnType("nvarchar(255)");
            builder.Property(s => s.Description).HasColumnType("nvarchar(500)");
            builder.Property(s => s.IsActive).HasDefaultValue(true);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.CreatedBy).HasColumnType("varchar(100)");
            builder.Property(s => s.ModifiedBy).HasColumnType("varchar(100)");
            builder.HasOne<QuestionGroupDTO>(s => s.QuestionGroup).WithMany(g => g.Questions).HasForeignKey(s => s.QuestionGroupId);
            builder.HasOne<QuestionTypeDTO>(s => s.QuestionType).WithMany(g => g.Questions).HasForeignKey(s => s.QuestionTypeId);
        }
    }
}
