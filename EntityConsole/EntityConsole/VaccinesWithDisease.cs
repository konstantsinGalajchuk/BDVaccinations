using System;
using System.Collections.Generic;

namespace EntityConsole;

public partial class VaccinesWithDisease
{
    public int VaccineId { get; set; }

    public string? Description { get; set; }

    public int DiseaseCode { get; set; }

    public string? DiseaseName { get; set; }
}
