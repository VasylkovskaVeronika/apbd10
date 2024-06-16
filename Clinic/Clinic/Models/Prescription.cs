using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }

    [ForeignKey(nameof(PatientId))]
    public Patient Patient { get; set; } = null!;
    [ForeignKey(nameof(DoctorId))]
    public Doctor Doctor { get; set; } = null!;
    
    public ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; } = new HashSet<Prescription_Medicament>();

}