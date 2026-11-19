using System.ComponentModel.DataAnnotations.Schema;

namespace Infectology.Library.Entities.Users;

public class UserSession
{
    public uint Id { get; set; }
    public required string Token { get; set; }
    public uint UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}