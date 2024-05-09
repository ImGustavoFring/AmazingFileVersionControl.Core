using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Models.UserDbEntities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public RoleInSystem RoleInSystem { get; set; } = RoleInSystem.USER;
        public Guid ProfileId { get; set; }
        public virtual ProfileEntity Profile { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;

    }

    public enum RoleInSystem
    {
        USER,
        ADMIN
    }
}
