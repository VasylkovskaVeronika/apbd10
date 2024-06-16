using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(PrescriptionId), nameof(MedicamentId))]
public class Prescription_Medicament
{
    public int PrescriptionId { get; set; }
    public int MedicamentId { get; set; }
    public int? Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }= string.Empty;

    [ForeignKey(nameof(PrescriptionId))]
    public Prescription Prescription { get; set; } = null!;
    [ForeignKey(nameof(MedicamentId))]
    public Medicament Medicament { get; set; } = null!;
}