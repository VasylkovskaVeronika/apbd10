using System.ComponentModel.DataAnnotations;

namespace Clinic.DTOs;

public class NewPrescriptionDTO
{
    public NewPatientDTO Patient { get; set; }
    public ICollection<NewMedicamentDTO> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public NewDoctorDTO Doctor { get; set; }
}

public class NewPatientDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}

public class NewMedicamentDTO
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}

public class NewDoctorDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}