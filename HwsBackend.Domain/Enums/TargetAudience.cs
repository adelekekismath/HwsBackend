namespace HwsBackend.Domain.Enums;

[Flags]
public enum TargetAudience
{
    None = 0,
    Seul = 1,
    Famille = 2,
    EntreAmis = 4,
    EnGroupe = 8
}