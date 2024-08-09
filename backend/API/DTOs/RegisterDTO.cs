namespace API.DTOs
{
    public class RegisterDTO
    {
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
