using System;
using System.Collections.Generic;

namespace EntityConsole;

public partial class Vaccination
{
    public int VaccinationId { get; set; }

    public int? VaccineId { get; set; }

    public DateTime? Date { get; set; }

    public int? DoseNum { get; set; }

    public int? PatientId { get; set; }

    public int? MedicalInstitutionId { get; set; }

    public virtual MedicalInstitution? MedicalInstitution { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Vaccine? Vaccine { get; set; }
}
