using System.ComponentModel.DataAnnotations;
using Okkam.Cars.WebApi.Validators;

namespace Okkam.Cars.WebApi.ViewModels;

/// <summary>
/// Модель представления для редактирования автомобиля.
/// </summary>
public class CarEditViewModel
{
    /// <summary>
    /// Идентификатор автомобиля.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Бренд.
    /// </summary>
    public int BrandId { get; set; }

    /// <summary>
    /// Название модели.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Идентификатор типа кузова.
    /// </summary>
    public int BodyStyleId { get; set; }

    /// <summary>
    /// Число сидений в салоне.
    /// </summary>
    [Range(1, 12)]
    public byte SeatsCount { get; set; }

    /// <summary>
    /// Url сайта официального дилера.
    /// </summary>
    [RuUrl]
    public string? Url { get; set; }

    /// <summary>
    /// Картинка.
    /// </summary>
    [AllowedExtensionsAttribute(new[] { ".jpg", ".png" })]
    public IFormFile? Picture { get; set; }

}