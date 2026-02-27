namespace HwsBackend.Application.DTOs;

using System.ComponentModel.DataAnnotations;
using HwsBackend.Domain.Enums;

public class ActivityUpdateDto
{
    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "La description est obligatoire.")]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "La catégorie est obligatoire.")]
    [Range(1, 17, ErrorMessage = "La catégorie doit être un ID valide.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "L'adresse est obligatoire.")]
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
    public string? PhoneNumber { get; set; }

    public string? OpeningHours { get; set; }
    public string? Website { get; set; }

    [Range(1, 90, ErrorMessage = "Le numéro de jour doit être compris entre 1 et 90.")]
    public int DayNumber { get; set; } 

    [Range(1, int.MaxValue, ErrorMessage = "L'ordre d'exécution doit être supérieur à 0.")]
    public int ExecutionOrder { get; set; }
}