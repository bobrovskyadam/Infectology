using System;
using System.Collections.Generic;
using System.Text;

namespace Infectology.Library.Entities.Content;

public class Image
{
    public uint Id { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
}
