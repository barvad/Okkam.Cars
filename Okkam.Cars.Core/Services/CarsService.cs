using Microsoft.Extensions.Logging;
using Okkam.Cars.Core.Contracts;
using Okkam.Cars.Core.Entities;

namespace Okkam.Cars.Core.Services;

public class CarsService : ICarsService
{
    private readonly ICarsRepository _carsRepository;
    private readonly IFilesRepository _filesRepository;
    private readonly ILogger _logger;

    public CarsService(ICarsRepository carsRepository,
        IFilesRepository filesRepository,
        ILoggerFactory loggerFactory)
    {
        _carsRepository = carsRepository;
        _filesRepository = filesRepository;
        _logger = loggerFactory.CreateLogger<CarsService>();
    }

    /// <summary>
    /// Добавляет автомобиль с приложенной картинкой.
    /// </summary>
    /// <param name="car">Автомобиль.</param>
    /// <param name="pictureData">Содержимое картинки.</param>
    /// <returns>Возвращает false если объект уже существует.</returns>
    public async Task<bool> AddCarAsync(Car car, byte[] pictureData)
    {
        if (!await _carsRepository.IsCarNotExists(car)) return false;
        var fileExtension = Path.GetExtension(car.Picture);
        car.Picture = await _filesRepository.AddFileAsync(pictureData, fileExtension);
        try
        {
            car = await _carsRepository.AddCarAsync(car);
        }
        catch
        {
            await TryRemoveFile(car.Picture);
            throw;
        }

        _logger.LogInformation(Strings.CarCreatedLogMessage, car.Name, car.Brand.Name, car.Created);
        return true;
    }

    /// <summary>
    /// Возвращает список автомобилей и их общее количество записей.
    /// </summary>
    /// <param name="page">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Общее количество записей и список автомобилей на странице.</returns>
    public async Task<(int, IEnumerable<Car>)> GetList(int page, int pageSize)
    {
        return await _carsRepository.GetList(page, pageSize);
    }

    /// <summary>
    /// Получает файл по имени.
    /// </summary>
    /// <param name="name">Имя файла.</param>
    /// <returns>Содержимое файла.</returns>
    public async Task<byte[]> GetFileAsync(string name)
    {
        return await _filesRepository.GetFileAsync(name);
    }

    /// <summary>
    /// Получает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Автомобиль.</returns>
    public async Task<Car> GetCarAsync(int id)
    {
        return await _carsRepository.GetCarByIdAsync(id);
    }

    /// <summary>
    /// Обновляет автомобиль.
    /// </summary>
    /// <param name="car">Автомобиль.</param>
    /// <param name="pictureData">Содержимое фотографии.</param>
    /// <returns>Возвращает false если объект уже существует.</returns>
    public async Task<bool> EditCarAsync(Car car, byte[] pictureData)
    {
        if (!await _carsRepository.IsCarNotExists(car, car.Id)) return false;
        var fileExtension = Path.GetExtension(car.Picture);
        if (pictureData != null! && pictureData.Length != 0)
            car.Picture = await _filesRepository.AddFileAsync(pictureData, fileExtension);
        try
        {
            (car, var pic) = await _carsRepository.UpdateCarAsync(car);

            if (pictureData != null! && pictureData.Length != 0)
                await TryRemoveFile(pic);
        }
        catch
        {
            if (pictureData != null! && pictureData.Length != 0)
                await TryRemoveFile(car.Picture);
            throw;
        }

        _logger.LogInformation(Strings.CarUpdatedLogMessage, car.Name, car.Brand.Name, car.Created);
        return true;
    }

    /// <summary>
    /// Удаляет автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Задача.</returns>
    public async Task DeleteCarAsync(int id)
    {
        var car = await _carsRepository.DeleteCarAsync(id);
        await TryRemoveFile(car.Picture);
        _logger.LogInformation(Strings.CarDeletedLogMessage, car.Name,
            car.Brand.Name, car.Created);
    }

    private async Task TryRemoveFile(string fileName)
    {
        try
        {
            await _filesRepository.RemoveFileAsync(fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, Strings.FailedToDeleteFile, fileName);
        }
    }
}