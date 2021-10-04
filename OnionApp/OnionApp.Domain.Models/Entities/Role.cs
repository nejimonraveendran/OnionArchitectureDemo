using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.Domain.Models.Entities
{
    public class Role : Entity
    {
        public enum RolesEnum
        {
            SuperAdmin = 1,
            UserAdmin = 2,
            User = 3
        }

        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
