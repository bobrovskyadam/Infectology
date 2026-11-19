using System;
using System.Collections.Generic;
using System.Text;

namespace Infectology.Library.Entities.Content;

public class Event
{
    public uint Id { get; set; }
    public required string Name { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly? End { get; set; }
    public string? Place { get; set; }
    public string? Link { get; set; }

    public uint AuthorId { get; set; }
}
