using System;

namespace OnionApp.Domain.Models.Entities
{
    public class UserEntity : EntityBase
    {
        public UserEntity()
        {
            Role = new RoleEntity();
        }

        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int? RoleId { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}
