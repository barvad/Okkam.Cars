using Okkam.Cars.Core.Entities;

namespace Okkam.Cars.Core.Contracts;

public interface IFiltersService
{
    /// <summary>
    /// Возвращает данные для фильтров по брэнду.
    /// </summary>
    /// <returns>Данные для фильтров по брэндуа</returns>
    Task<Brand[]> GetBrandsAsync();


    /// <summary>
    /// Возвращает данные для фильтров по типу кузова.
    /// </summary>
    /// <returns>Данные для фильтров по типу кузова</returns>
    Task<BodyStyle[]> GetBodyStylesAsync();
}