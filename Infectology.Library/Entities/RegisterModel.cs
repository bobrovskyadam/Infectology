using System;
using System.Collections.Generic;
using System.Text;

namespace Infectology.Library.Entities;

public class RegisterModel
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateTime? BirthDate { get; set; }

    public string TitleBefore { get; set; } = "";
    public string TitleAfter { get; set; } = "";

    public string Address { get; set; } = "";
    public string Zip { get; set; } = "";
    public string PhonePrefix { get; set; } = "+421";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public string City { get; set; } = "";
    public string Fax { get; set; } = "";

    public string Password { get; set; } = "";
    public string ConfirmPassword { get; set; } = "";

    public string WorkAddress { get; set; } = "";
    public string WorkZip { get; set; } = "";
    public string WorkCity { get; set; } = "";
    public string WorkFax { get; set; } = "";
    public string WorkPhonePrefix { get; set; } = "+421";
    public string WorkPhone { get; set; } = "";
    public string WorkEmail { get; set; } = "";

    public string Position { get; set; } = "";
    public string Specialization { get; set; } = "";

    public int? GraduationYear { get; set; }
    public string Chamber { get; set; } = "";
    public string LicenseId { get; set; } = "";

    public bool AgreeMembership { get; set; }
    public bool AgreePrivacy { get; set; }
    public bool AgreeRules { get; set; }
    public bool ConfirmTruth { get; set; }
}
