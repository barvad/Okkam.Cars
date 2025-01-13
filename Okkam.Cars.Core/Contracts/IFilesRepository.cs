namespace Okkam.Cars.Core.Contracts;

public interface IFilesRepository
{
    /// <summary>
    /// Добавляет файл в хранилище.
    /// </summary>
    /// <param name="fileData">Содержимое файла.</param>
    /// <param name="fileExtension">Расширение файла.</param>
    /// <returns>Имя файла.</returns>
    Task<string> AddFileAsync(byte[] fileData, string fileExtension);

    /// <summary>
    /// Удаляет файл из хранилища.
    /// </summary>
    /// <param name="picture">Имя файла.</param>
    /// <returns>Задача.</returns>
    Task RemoveFileAsync(string picture);

    /// <summary>
    /// Получает файл по имени.
    /// </summary>
    /// <param name="name">Имя файла.</param>
    /// <returns>Содержимое файла.</returns>
    Task<byte[]> GetFileAsync(string name);
}