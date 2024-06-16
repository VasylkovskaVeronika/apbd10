using Clinic.Models;

namespace Clinic.Services;

public interface IDbService
{
    Task<ICollection<Patient>> GetPatientsData(string? clientLastName);
    Task<bool> DoesPatientExist(int patientID);
    Task<bool> DoesDoctorExist(string doctorName);
    Task<bool> DoesMedicamentExist(int MedicamentID);
    Task AddNewPrescription(Prescription p);
    Task AddNewPatient(Patient p);
    Task<Medicament?> GetMedicamentById(int id);
    Task<Doctor?> GetDoctorByLastName(string doctorName);
    Task AddPrescription_Medicament(IEnumerable<Prescription_Medicament> Precription_Medicaments);
}