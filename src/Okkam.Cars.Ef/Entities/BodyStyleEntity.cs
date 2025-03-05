namespace Okkam.Cars.Ef.Entities;

/// <summary>
/// Тип кузова.
/// </summary>
public class BodyStyleEntity
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название типа кузова.
    /// </summary>
    public string Name { get; set; }
}