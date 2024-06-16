using System.Transactions;
using Clinic.DTOs;
using Clinic.Models;
using Clinic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clinic;

[Route("api/[controller]")]
[ApiController]
public class PatientController :ControllerBase
{
    private readonly IDbService _dbService;
    public PatientController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPatientData(string? patientLastName = null)
    {
        var patients = await _dbService.GetPatientsData(patientLastName);
        
        return Ok(patients.Select(e => new GetPatientDataDTO()
        {
            IdPatient = e.IdPatient,
            FirstName = e.FirstName,
            LastName = e.LastName,
            BirthDate = e.BirthDate,
            Prescriptions = e.Prescriptions.Select(p => new GetPrescriptionsMedicamentsDTO()
            {
               IdPrescription = p.IdPrescription,
               Date = p.Date,
               DueDate = p.DueDate,
               Medicaments = p.PrescriptionMedicaments.Select(pm=>
                   new GetMedicamentsDTO()
                   {
                       IdMedicament = pm.MedicamentId,
                       Name = pm.Medicament.Name,
                       Dose= pm.Dose.GetValueOrDefault(0),
                       Description = pm.Details
                   }).ToList(),
               Doctor = new GetDoctorDTO()
               {
                   IdDoctor = p.Doctor.Id,
                   FirstName = p.Doctor.FirstName
               }
            }).OrderBy(e=> e.DueDate).ToList()
        }));
    }
    
    [HttpPost("{patientID}/prescriptions")]
    public async Task<IActionResult> AddNewOrder(int patientID, NewPrescriptionDTO newPrescription)
    {
        if (!await _dbService.DoesDoctorExist(newPrescription.Doctor.LastName))
            return NotFound($"Doctor with given last name - {newPrescription.Doctor.LastName} doesn't exist");
        if (newPrescription.Medicaments.Count > 10)
        {
            return Forbid($"Forbidden to issue more than 10 medicaments");
        }

        if (newPrescription.DueDate >= newPrescription.Date)
        {
            return Forbid($"DueDate should be >= Date");
        }

        
        var Prescription = new Prescription()
        {
            DoctorId = _dbService.GetDoctorByLastName(newPrescription.Doctor.LastName).Id,
            
        };
    
        var medicaments = new List<Prescription_Medicament>();
        foreach (var newMedicament in newPrescription.Medicaments)
        {
            var Medicament = await _dbService.GetMedicamentById(newMedicament.IdMedicament);
            if(Medicament is null)
                return NotFound($"Medicament with ID - {newMedicament.IdMedicament} doesn't exist");
    
            medicaments.Add(new Prescription_Medicament
            {
                MedicamentId = Medicament.IdMedicament,
                Dose = newMedicament.Dose,
                Details = newMedicament.Description,
                Prescription = Prescription
            });
        }
    
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _dbService.AddNewPrescription(Prescription);
            if (!await _dbService.DoesPatientExist(patientID))
            {
                var patient = new Patient()
                {
                    IdPatient = newPrescription.Patient.IdPatient,
                    FirstName = newPrescription.Patient.FirstName,
                    LastName = newPrescription.Patient.LastName,
                    BirthDate = newPrescription.Patient.BirthDate
                };
                await _dbService.AddNewPatient(patient);
            }
            await _dbService.AddPrescription_Medicament(medicaments);
    
            scope.Complete();
        }
    
        return Created("api/prescriptions", new
        {
            Id = Prescription.IdPrescription,
            Prescription.DueDate,
            Prescription.Date
        });
    }
}