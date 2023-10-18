using System;
using System.Collections.Generic;

namespace EntityConsole;

public partial class MedicalInstitution
{
    public int MedicalInstitutionId { get; set; }

    public string? Name { get; set; }

    public string? Adress { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
