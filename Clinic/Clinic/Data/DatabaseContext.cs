using Microsoft.EntityFrameworkCore;
using Clinic.Models;
namespace Clinic.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
}