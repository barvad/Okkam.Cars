using Okkam.Cars.Core.Entities;

namespace Okkam.Cars.Core.Contracts;

public interface ICarsService
{
    /// <summary>
    /// Добавляет автомобиль с приложенной картинкой.
    /// </summary>
    /// <param name="car">Автомобиль.</param>
    /// <param name="pictureData">Содержимое картинки.</param>
    /// <returns>Возвращает false если объект уже существует.</returns>
    Task<bool> AddCarAsync(Car car, byte[] pictureData);

    /// <summary>
    /// Возвращает список автомобилей и их общее количество записей.
    /// </summary>
    /// <param name="page">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Общее количество записей и список автомобилей на странице.</returns>
    Task<(int, IEnumerable<Car>)> GetList(int page, int pageSize);

    /// <summary>
    /// Получает файл по имени.
    /// </summary>
    /// <param name="name">Имя файла.</param>
    /// <returns>Содержимое файла.</returns>
    Task<byte[]> GetFileAsync(string name);

    /// <summary>
    /// Получает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Автомобиль.</returns>
    Task<Car> GetCarAsync(int id);


    /// <summary>
    /// Обновляет автомобиль.
    /// </summary>
    /// <param name="car">Автомобиль.</param>
    /// <param name="pictureData">Содержимое фотографии.</param>
    /// <returns>Возвращает false если объект уже существует.</returns>
    Task<bool> EditCarAsync(Car car, byte[] pictureData);

    /// <summary>
    /// Удаляет автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Задача.</returns>
    Task DeleteCarAsync(int id);
}