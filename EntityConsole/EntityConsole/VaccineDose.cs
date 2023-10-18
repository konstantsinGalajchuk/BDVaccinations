using System;
using System.Collections.Generic;

namespace EntityConsole;

public partial class VaccineDose
{
    public int VaccineDoseId { get; set; }

    public int? DoseId { get; set; }

    public int? VaccineId { get; set; }

    public virtual Dose? Dose { get; set; }

    public virtual Vaccine? Vaccine { get; set; }
}
