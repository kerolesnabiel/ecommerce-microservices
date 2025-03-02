namespace UserService.Domain.Entities;

public class User {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = string.Empty;

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }

    public string Role { get; set; } = "USER";
    public bool IsActive { get; set; } = true;

    public string? StoreName { get; set; }
    public string? TaxId { get; set; }
    public string? StoreAddress { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}