namespace WebAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int PhoneNo { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
    }
}
