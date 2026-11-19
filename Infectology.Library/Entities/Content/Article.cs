using Infectology.Library.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infectology.Library.Entities.Content;

public class Article
{
    public uint Id { get; set; }
    public required string Heading { get; set; }
    public string? HeadingImage { get; set; }
    public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;
    public DateTime? DateTimeEdited { get; set; }
    public DateTime? DateTimeDeleted { get; set; }
    public bool IsDeleted => DateTimeDeleted != null;
    public required string Markdown { get; set; }

    public required uint AuthorId { get; set; }
    public required User Author { get; set; }
}
