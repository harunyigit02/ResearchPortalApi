﻿namespace FinalProjectWebApi.Entities.Concrete
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Role { get; set; }
        
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    }
}
