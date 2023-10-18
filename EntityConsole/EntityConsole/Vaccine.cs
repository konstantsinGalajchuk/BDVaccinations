using System;
using System.Collections.Generic;

namespace EntityConsole;

public partial class Vaccine
{
    public int VaccineId { get; set; }

    public int? DiseaseId { get; set; }

    public string? Description { get; set; }

    public string? Manufacturer { get; set; }

    public virtual Disease? Disease { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();

    public virtual ICollection<VaccineDose> VaccineDoses { get; set; } = new List<VaccineDose>();
}
