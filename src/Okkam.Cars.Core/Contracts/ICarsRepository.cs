using Okkam.Cars.Core.Entities;

namespace Okkam.Cars.Core.Contracts;

/// <summary>
/// Репозиторий автомобилей.
/// </summary>
public interface ICarsRepository
{
    /// <summary>
    /// Добавляет автомобиль.
    /// </summary>
    /// <param name="car">Автомобиль.</param>
    Task<Car> AddCarAsync(Car car);

    /// <summary>
    /// Получает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Автомобиль.</returns>
    Task<Car> GetCarByIdAsync(int id);

    /// <summary>
    /// Проверяет был ли ранее добавлен автомобиль. 
    /// </summary>
    /// <param name="car">Автомобиль.</param>
    /// <param name="id"></param>
    /// <returns>Был ли ранее добавлен автомобиль.</returns>
    Task<bool> IsCarNotExists(Car car,int? id =null);

    /// <summary>
    /// Возвращает список автомобилей и их общее количество записей.
    /// </summary>
    /// <param name="page">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Общее количество записей и список автомобилей на странице.</returns>
    Task<(int, IEnumerable<Car>)> GetList(int page, int pageSize);

    /// <summary>
    /// Обновляет автомобиль.
    /// </summary>
    /// <param name="car">Автомобиль.</param>
    /// <returns>Измененный автомобиль и адрес предыдущей картинки.</returns>
    Task<(Car, string)> UpdateCarAsync(Car car);

    /// <summary>
    /// Удаляет автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Задача.</returns>
    Task<Car> DeleteCarAsync(int id);
}