namespace HwsBackend.Application.DTOs;

using System.ComponentModel.DataAnnotations;
using HwsBackend.Domain.Enums;

public class GuideCreateDto
{
    [Required(ErrorMessage = "Le titre du guide est obligatoire.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Le titre doit contenir entre 3 et 100 caractères.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "La description est obligatoire.")]
    [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères.")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(1, 90, ErrorMessage = "La durée du voyage doit être comprise entre 1 et 90 jours.")]
    public int DaysCount { get; set; }

    [Required(ErrorMessage = "Le type de mobilité est requis.")]
    public List<int> MobilityIds { get; set; } = new();

    [Required(ErrorMessage = "La saison est requise.")]
    public List<int> SeasonIds { get; set; } = new();

    [Required(ErrorMessage = "Le public cible est requis.")]
    public List<int> TargetAudienceIds { get; set; } = new();
}