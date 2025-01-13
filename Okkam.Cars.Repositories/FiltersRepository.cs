using Microsoft.EntityFrameworkCore;
using Okkam.Cars.Core.Contracts;
using Okkam.Cars.Core.Entities;
using Okkam.Cars.Ef;

namespace Okkam.Cars.Repositories;

public class FiltersRepository : IFiltersRepository
{
    private readonly CarsDbContext _dbContext;

    public FiltersRepository(CarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Возвращает данные для фильтров по брэнду.
    /// </summary>
    /// <returns>Данные для фильтров по брэндуа</returns>
    public async Task<Brand[]> GetBrandsAsync()
    {
        return await _dbContext.Brands.OrderBy(b => b.Name)
            .Select(b => new Brand { Id = b.Id, Name = b.Name })
            .ToArrayAsync();
    }

    /// <summary>
    /// Возвращает данные для фильтров по типу кузова.
    /// </summary>
    /// <returns>Данные для фильтров по типу кузова</returns>
    public async Task<BodyStyle[]> GetBodyStylesAsync()
    {
        return await _dbContext.BodyStyles.OrderBy(b => b.Name)
            .Select(b => new BodyStyle { Id = b.Id, Name = b.Name })
            .ToArrayAsync();
    }
}