using System;
using System.Collections.Generic;

namespace EntityConsole;

public partial class Patient
{
    public int PatientId { get; set; }

    public string? Fio { get; set; }

    public string? Sex { get; set; }

    public string Pasport { get; set; } = null!;

    public string? Adress { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
