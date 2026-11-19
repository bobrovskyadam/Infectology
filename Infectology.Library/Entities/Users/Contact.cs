using Infectology.Library.Entities.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infectology.Library.Entities.Users;

public class Contact
{
    public uint Id { get; set; }
    public required string EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? LandlineNumber { get; set; }
    public string? Fax { get; set; }
}
