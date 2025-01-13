using Okkam.Cars.Core.Contracts;
using Okkam.Cars.Core.Entities;

namespace Okkam.Cars.Core.Services;

public class FiltersService : IFiltersService
{
    private readonly IFiltersRepository _repository;

    public FiltersService(IFiltersRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Возвращает данные для фильтров по брэнду.
    /// </summary>
    /// <returns>Данные для фильтров по брэндуа</returns>
    public async Task<Brand[]> GetBrandsAsync()
    {
        return await _repository.GetBrandsAsync();
    }

    /// <summary>
    /// Возвращает данные для фильтров по типу кузова.
    /// </summary>
    /// <returns>Данные для фильтров по типу кузова</returns>
    public async Task<BodyStyle[]> GetBodyStylesAsync()
    {
        return await _repository.GetBodyStylesAsync();
    }
}