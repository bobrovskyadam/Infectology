using System;
using System.Collections.Generic;
using System.Text;

namespace Infectology.Library.DTOs.Requests.Content;

public record DocumentUpdateRequest(uint Id, string Name);
