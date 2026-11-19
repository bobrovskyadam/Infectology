using System;
using System.Collections.Generic;
using System.Text;

namespace Infectology.Library.Entities.Content;

public class SpecialistContact
{
    public required uint Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Category { get; set; }

    public string? Position { get; set; }
    public string? City { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
}
 