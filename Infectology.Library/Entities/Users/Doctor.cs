using System;
using System.Collections.Generic;
using System.Text;

namespace Infectology.Library.Entities.Users;

public class Doctor : User
{
    public required DateOnly DateOfBirth { get; set; }
    public List<string> Titles { get; set; } = [];
    public required int GraduationYear { get; set; }
    public required string WorkplaceFunction { get; set; }
    public required string Focus { get; set; }
    public required string Chamber { get; set; }
    public required string ChamberId { get; set; }

    public uint? WorkplaceContactId { get; set; }
    public Contact? WorkplaceContact { get; set; }

    public uint? WorkplaceId { get; set; }
    public Address? Workplace { get; set; }

}