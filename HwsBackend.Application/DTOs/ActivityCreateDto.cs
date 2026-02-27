namespace HwsBackend.Application.DTOs;

using System.ComponentModel.DataAnnotations; 

public class ActivityCreateDto
{
    [Required(ErrorMessage = "Le titre de l'activité est obligatoire.")]
    [MaxLength(100, ErrorMessage = "Le titre de l'activité doit avoir moins de 100 caractères.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "La description de l'activité est obligatoire.")]
    [MaxLength(500, ErrorMessage = "La description de l'activité doit avoir moins de 500 caractères.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "La catégorie de l'activité est obligatoire.")]
    [Range(1, 17, ErrorMessage = "La catégorie de l'activité doit être un ID valide.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "L'adresse de l'activité est obligatoire.")]
    [MaxLength(200, ErrorMessage = "L'adresse doit avoir moins de 200 caractères.")]
    public string Address { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(100)]
    public string? OpeningHours { get; set; }

    [Url(ErrorMessage = "L'URL du site web n'est pas valide.")]
    [MaxLength(255)]
    public string? Website { get; set; }

    [Required]
    [Range(1, 90, ErrorMessage = "Le numéro du jour doit être compris entre 1 et 90.")]
    public int DayNumber { get; set; } 

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "L'ordre d'exécution doit être supérieur à 0.")]
    public int ExecutionOrder { get; set; } 

    [Required(ErrorMessage = "L'ID du guide est obligatoire pour lier l'activité.")]
    public int GuideId { get; set; }
}