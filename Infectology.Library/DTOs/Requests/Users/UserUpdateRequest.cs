using Infectology.Library.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infectology.Library.DTOs.Requests.Users;

public record UserUpdateRequest(
    uint Id,
    Role Role,
    bool IsVerified
    );