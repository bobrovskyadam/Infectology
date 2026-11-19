using Infectology.Library.Entities.Users;

namespace Infectology.Library.Entities.Content;

public class Document
{
    public uint Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required string Category { get; set; }

    public required string FileName { get; set; }
    public uint AuthorId { get; set; }
    public required User Author { get; set; }

}