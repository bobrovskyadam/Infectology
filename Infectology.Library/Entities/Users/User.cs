using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Infectology.Library.Entities.Users;

public class User
{
    public uint Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PasswordHash { get; set; }
    public Role Role { get; set; }

    
    public uint PersonalContactId { get; set; }
    public required Contact PersonalContact { get; set; }

    public uint? ResidenceId { get; set; }
    public Address? Residence { get; set; }

    public uint? VerifiedByUserId { get; set; }
    public User? VerifiedByUser { get; set; }

    public DateTime? DateTimeVerified { get; set; }

    [NotMapped, JsonIgnore]
    public bool IsVerified => DateTimeVerified != null && VerifiedByUser != null;

    public DateTime DateTimeCreated { get; init; } = DateTime.UtcNow;
}
