using System;
using System.Collections.Generic;

namespace EntityConsole;

public partial class Dose
{
    public int DoseId { get; set; }

    public double Value { get; set; }

    public virtual ICollection<VaccineDose> VaccineDoses { get; set; } = new List<VaccineDose>();
}
