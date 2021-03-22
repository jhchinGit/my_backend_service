namespace MyBackendService.Models.DTOs
{
    public record AuthenticationCodeDto
    {
        public string AuthenticationCode { get; init; }
        public string Username { get; init; }
    }
}