using System;
using System.Collections.Generic;

namespace EntityConsole;

public partial class Disease
{
    public int DiseaseId { get; set; }

    public int Code { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
}
