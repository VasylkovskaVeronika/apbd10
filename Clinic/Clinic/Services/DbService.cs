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
    
}