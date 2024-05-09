using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AmazingFileVersionControl.Core.Models.UserDbEntities;

namespace AmazingFileVersionControl.Core.Contexts.EntityConfigurations
{
    public class ProfileEntityConfiguration : IEntityTypeConfiguration<ProfileEntity>
    {
        public void Configure(EntityTypeBuilder<ProfileEntity> builder)
        {
            builder
            .HasKey(p => p.Id);

            builder
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<ProfileEntity>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
