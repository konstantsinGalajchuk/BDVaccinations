using System;
using System.Collections.Generic;

namespace EntityConsole;

public partial class VaccinesWithDose
{
    public int VaccineId { get; set; }

    public int? DiseaseId { get; set; }

    public double Dose { get; set; }
}
