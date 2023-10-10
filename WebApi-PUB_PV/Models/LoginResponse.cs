namespace WebApi_PUB_PV.Models
{
    public class LoginResponse
    {
        public bool StatusOk { get; set; }
        public string? StatusMessage { get; set; }
        public string? IdUsuario { get; set; }
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public int ExpirationMinutes { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
    }
}
