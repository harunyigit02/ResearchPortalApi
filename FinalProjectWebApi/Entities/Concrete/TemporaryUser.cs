namespace FinalProjectWebApi.Entities.Concrete
{
    public class TemporaryUser
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        public string VerificationCode { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Role { get; set; }
    }
}
