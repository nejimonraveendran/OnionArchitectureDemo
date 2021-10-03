using System;

namespace OnionApp.Domain.Models.Entities
{
    public class UserEntity 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
