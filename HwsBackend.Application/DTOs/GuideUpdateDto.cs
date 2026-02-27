namespace HwsBackend.Application.DTOs;

using System.ComponentModel.DataAnnotations;

public class GuideUpdateDto
{
    [Required(ErrorMessage = "Le titre du guide est obligatoire.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Le titre doit comporter entre 3 et 100 caractères.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "La description du guide est obligatoire.")]
    [MaxLength(1000, ErrorMessage = "La description ne doit pas dépasser 1000 caractères.")]
    public string Description { get; set; } = string.Empty;

    [Range(1, 90, ErrorMessage = "Le nombre de jours doit être compris entre 1 et 90.")]
    public int DaysCount { get; set; }

    [Required(ErrorMessage = "Le type de mobilité est obligatoire.")]
    public List<int> MobilityIds { get; set; } = new();

    [Required(ErrorMessage = "La saison est obligatoire.")]
    public List<int> SeasonIds { get; set; } = new();

    [Required(ErrorMessage = "L'audience cible est obligatoire.")]
    public List<int> TargetAudienceIds { get; set; } = new();
}