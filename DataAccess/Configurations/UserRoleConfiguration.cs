using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleDTO>
    {
        public void Configure(EntityTypeBuilder<UserRoleDTO> builder)
        {
            builder.ToTable("TBSytem_UserRoles");
        }
    }
}
