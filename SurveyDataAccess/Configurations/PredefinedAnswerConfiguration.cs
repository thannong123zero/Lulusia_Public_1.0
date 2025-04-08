using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyDataAccess.DTOs;

namespace SurveyDataAccess.Configurations
{
    public class PredefinedAnswerConfiguration : IEntityTypeConfiguration<PredefinedAnswerDTO>
    {
        public void Configure(EntityTypeBuilder<PredefinedAnswerDTO> builder)
        {
            builder.ToTable("Table_PredefinedAnswers");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.NameEN).HasColumnType("nvarchar(255)");
            builder.Property(s => s.NameVN).HasColumnType("nvarchar(255)");
            builder.Property(s => s.Description).HasColumnType("nvarchar(500)");
            builder.Property(s => s.Point).HasColumnType("tinyint");
            builder.HasOne<QuestionDTO>(s => s.Question).WithMany(g => g.PredefinedAnswers).HasForeignKey(s => s.QuestionId);
        }
    }
}
