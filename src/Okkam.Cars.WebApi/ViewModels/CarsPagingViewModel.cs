using Okkam.Cars.WebApi.Validators;

namespace Okkam.Cars.WebApi.ViewModels
{
    /// <summary>
    /// Модель представления для отображения списка автомобилей.
    /// </summary>
    public class CarsPagingViewModel
    {
        /// <summary>
        /// Всего страниц.
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Всего автомобилей.
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Автомобили на текущей странице.
        /// </summary>
        public IEnumerable<CarListItemViewModel> Cars { get; set; }
    }
}
