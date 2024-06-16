﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Type { get; set; } = string.Empty;
    public ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; } = new HashSet<Prescription_Medicament>();

}