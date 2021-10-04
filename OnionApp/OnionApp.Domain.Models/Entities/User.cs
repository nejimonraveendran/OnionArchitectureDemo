using System;

namespace OnionApp.Domain.Models.Entities
{
    public class User : Entity
    {
        public User()
        {
            Role = new Role();
        }

        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
