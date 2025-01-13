using Okkam.Cars.Core.Entities;

namespace Okkam.Cars.Core.Contracts;

public interface IFiltersRepository
{
    /// <summary>
    /// Возвращает данные для фильтров по брэнду.
    /// </summary>
    /// <returns>Данные для фильтров по брэндуа</returns>
    public Task<Brand[]> GetBrandsAsync();

    /// <summary>
    /// Возвращает данные для фильтров по типу кузова.
    /// </summary>
    /// <returns>Данные для фильтров по типу кузова</returns>
    public Task<BodyStyle[]> GetBodyStylesAsync();
}