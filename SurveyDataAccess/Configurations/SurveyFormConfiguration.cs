using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyDataAccess.DTOs;

namespace SurveyDataAccess.Configurations
{
    public class SurveyFormConfiguration : IEntityTypeConfiguration<SurveyFormDTO>
    {
        public void Configure(EntityTypeBuilder<SurveyFormDTO> builder)
        {
            builder.ToTable("Table_SurveyForms");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.NameEN).HasColumnType("nvarchar(255)");
            builder.Property(s => s.NameVN).HasColumnType("nvarchar(255)");
            builder.Property(s => s.TitleEN).HasColumnType("nvarchar(255)");
            builder.Property(s => s.TitleVN).HasColumnType("nvarchar(255)");
            builder.Property(s => s.DescriptionEN).HasColumnType("nvarchar(1000)");
            builder.Property(s => s.DescriptionVN).HasColumnType("nvarchar(1000)");
            builder.Property(s => s.CreatedBy).HasColumnType("varchar(100)");
            builder.Property(s => s.ModifiedBy).HasColumnType("varchar(100)");
        }
    }
}
