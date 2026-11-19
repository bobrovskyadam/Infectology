using System;
using System.Collections.Generic;
using System.Text;

namespace Infectology.Library.DTOs.Responses.Content;

public record DocumentResponse(
    uint Id,
    string Name,
    string? Description,
    string Category,
    string FileName
)
{
        public long Size { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeEdited { get; set; }
}