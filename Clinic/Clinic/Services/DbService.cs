using Clinic.Data;
using Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<ICollection<Patient>> GetPatientsData(string? patientLastName)
    {
        return await _context.Patients
            .Include(e => e.Prescriptions)
            .ThenInclude(e => e.PrescriptionMedicaments)
            .ThenInclude(e=> e.Medicament)
            .Include(e=>e.Prescriptions)
            .ThenInclude(e=> e.Doctor)
            .Where(e => patientLastName == null || e.LastName == patientLastName)
            .ToListAsync();
    }

    public async Task<bool> DoesPatientExist(int patientID)
    {
        return await _context.Patients.AnyAsync(e => e.IdPatient == patientID);
    }

    public async Task<bool> DoesDoctorExist(string doctorName)
    {
        return await _context.Doctors.AnyAsync(e => e.LastName==doctorName);
    }

    public async Task<bool> DoesMedicamentExist(int MedicamentID)
    {
        return await _context.Medicaments.AnyAsync(e => e.IdMedicament == MedicamentID);
    }

    public async Task AddNewPrescription(Prescription p)
    {
        await _context.AddAsync(p);
        await _context.SaveChangesAsync();
    }

    public async Task AddNewPatient(Patient p)
    {
        await _context.AddAsync(p);
        await _context.SaveChangesAsync();
    }

    public async Task<Medicament?> GetMedicamentById(int id)
    {
        return await _context.Medicaments.FirstOrDefaultAsync(e => e.IdMedicament== id);
    }

    public async Task AddPrescription_Medicament(IEnumerable<Prescription_Medicament> Precription_Medicaments)
    {
        await _context.AddRangeAsync(Precription_Medicaments);
        await _context.SaveChangesAsync();
    }

    public async Task<Doctor?> GetDoctorByLastName(string doctorName)
    {
        return await _context.Doctors.FirstOrDefaultAsync(e => e.LastName==doctorName);
    }
}