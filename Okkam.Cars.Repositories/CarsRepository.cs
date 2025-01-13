using Microsoft.EntityFrameworkCore;
using Okkam.Cars.Core.Contracts;
using Okkam.Cars.Core.Entities;
using Okkam.Cars.Core.Exceptions;
using Okkam.Cars.Ef;
using Okkam.Cars.Ef.Entities;

namespace Okkam.Cars.Repositories;

/// <summary>
/// Репозиторий автомобилей.
/// </summary>
public class CarsRepository : ICarsRepository
{
    private readonly CarsDbContext _dbContext;

    public CarsRepository(CarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Добавляет автомобиль.
    /// </summary>
    /// <param name="car">Автомобиль.</param>
    public async Task<Car> AddCarAsync(Car car)
    {
        var carEntity = new CarEntity
        {
            BodyStyleId = car.BodyStyle.Id,
            BrandId = car.Brand.Id,
            Name = car.Name,
            Picture = car.Picture,
            Url = car.Url,
            SeatsCount = car.SeatsCount
        };
        _dbContext.Add(carEntity);
        await _dbContext.SaveChangesAsync();
        return await GetCarByIdAsync(carEntity.Id);
    }

    /// <summary>
    /// Получает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Автомобиль.</returns>
    public async Task<Car> GetCarByIdAsync(int id)
    {
        var carEntity = await _dbContext.Cars
            .AsNoTracking()
            .Include(car => car.Brand)
            .Include(car => car.BodyStyle)
            .FirstOrDefaultAsync(car => car.Id == id);
        if (carEntity == null) throw new ObjectNotFoundException();
        return Convert(carEntity);
    }

    /// <summary>
    /// Проверяет был ли ранее добавлен автомобиль.
    /// </summary>
    /// <param name="car">Автомобиль.</param>
    /// <param name="id"></param>
    /// <returns>Был ли ранее добавлен автомобиль.</returns>
    public async Task<bool> IsCarNotExists(Car car, int? id = null)
    {
        if (id.HasValue)
            return !await _dbContext
                .Cars
                .AnyAsync(carEntity => carEntity.BrandId == car.Brand.Id
                                       && carEntity.BodyStyleId == car.BodyStyle.Id
                                       && carEntity.SeatsCount == car.SeatsCount
                                       && carEntity.Name == car.Name && car.Id != id.Value);
        return !await _dbContext
            .Cars
            .AnyAsync(carEntity => carEntity.BrandId == car.Brand.Id
                                   && carEntity.BodyStyleId == car.BodyStyle.Id
                                   && carEntity.SeatsCount == car.SeatsCount
                                   && carEntity.Name == car.Name);
    }

    /// <summary>
    /// Возвращает список автомобилей и их общее количество записей.
    /// </summary>
    /// <param name="page">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Общее количество записей и список автомобилей на странице.</returns>
    public async Task<(int, IEnumerable<Car>)> GetList(int page, int pageSize)
    {
        var result = _dbContext.Cars
            .AsNoTracking()
            .Include(car => car.BodyStyle)
            .Include(car => car.Brand)
            .OrderByDescending(car => car.Created).Skip(pageSize * (page - 1)).Take(pageSize);
        var entities = await result.ToArrayAsync();
        var count = await _dbContext.Cars.CountAsync();
        return (count, entities.Select(Convert));
    }

    /// <summary>
    /// Обновляет автомобиль.
    /// </summary>
    /// <param name="car">Автомобиль.</param>
    /// <returns>Измененный автомобиль и адрес предыдущей картинки.</returns>
    public async Task<(Car, string)> UpdateCarAsync(Car car)
    {
        var carEntity = _dbContext.Cars.FirstOrDefault(c => c.Id == car.Id);
        if (carEntity == null) throw new ObjectNotFoundException();
        var picture = carEntity.Picture;
        carEntity.BodyStyleId = car.BodyStyle.Id;
        carEntity.BrandId = car.Brand.Id;
        carEntity.Name = car.Name;
        carEntity.Picture = car.Picture ?? carEntity.Picture;
        carEntity.Url = car.Url;
        carEntity.SeatsCount = car.SeatsCount;

        await _dbContext.SaveChangesAsync();
        return (await GetCarByIdAsync(carEntity.Id), picture);
    }

    /// <summary>
    /// Удаляет автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Задача.</returns>
    public async Task<Car> DeleteCarAsync(int id)
    {
        var carEntity = await _dbContext.Cars
            .Include(car => car.BodyStyle)
            .Include(car => car.Brand)
            .FirstOrDefaultAsync(car => car.Id == id);
        if (carEntity == null) throw new ObjectNotFoundException();
        var car = Convert(carEntity);
        _dbContext.Cars.Remove(carEntity);
        await _dbContext.SaveChangesAsync();
        return car;
    }

    private Car Convert(CarEntity carEntity)
    {
        return new Car
        {
            Id = carEntity.Id,
            Brand = new Brand
            {
                Id = carEntity.Brand.Id,
                Name = carEntity.Brand.Name
            },
            BodyStyle = new BodyStyle
            {
                Id = carEntity.BodyStyle.Id,
                Name = carEntity.BodyStyle.Name
            },
            Name = carEntity.Name,
            Created = carEntity.Created,
            Picture = carEntity.Picture,
            Url = carEntity.Url,
            SeatsCount = carEntity.SeatsCount
        };
    }
}