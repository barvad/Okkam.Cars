using Okkam.Cars.Ef.Entities;
using Okkam.Cars.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace Okkam.Cars.WebApi.ViewModels
{
    /// <summary>
    /// Модель представления для добавления автомобиля.
    /// </summary>
    public class CarAddViewModel
    {
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
        [AllowedExtensionsAttribute(new []{".jpg",".png"})]
        public IFormFile Picture { get; set; }

    }
}
