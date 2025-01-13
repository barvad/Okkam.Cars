using System.ComponentModel.DataAnnotations;

namespace Okkam.Cars.WebApi.ViewModels;

/// <summary>
/// Модель представления для автомобиля автомобиля в списке.
/// </summary>
public class CarListItemViewModel
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Бренд.
    /// </summary>
    public string Brand { get; set; }

    /// <summary>
    /// Название модели.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Тип кузова.
    /// </summary>
    public string BodyStyle { get; set; }

    /// <summary>
    /// Число сидений в салоне.
    /// </summary>
    [Range(1, 12)]
    public byte SeatsCount { get; set; }
}