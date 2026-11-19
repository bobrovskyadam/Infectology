using System;
using System.Collections.Generic;
using System.Text;

namespace Infectology.Library.Entities.Users;

public class Address
{
    public uint Id { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
}
