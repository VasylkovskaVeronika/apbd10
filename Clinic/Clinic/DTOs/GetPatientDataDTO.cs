namespace Clinic.DTOs;

public class GetPatientDataDTO
{
   
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<GetPrescriptionsMedicamentsDTO> Prescriptions { get; set; } = null!;
    }

    public class GetPrescriptionsMedicamentsDTO
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public ICollection<GetMedicamentsDTO> Medicaments { get; set; } = null!;
        public GetDoctorDTO Doctor { get; set; }
    }
    public class GetMedicamentsDTO
    {
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public int Dose { get; set; }
        public string Description { get; set; }
    }
    public class GetDoctorDTO
    {
        public int IdDoctor { get; set; }
        public string FirstName { get; set; }
    }

