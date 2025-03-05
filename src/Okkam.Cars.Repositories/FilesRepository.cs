using Minio;
using Minio.DataModel.Args;
using Okkam.Cars.Core;
using Okkam.Cars.Core.Contracts;

namespace Okkam.Cars.Repositories;

public class FilesRepository : IFilesRepository
{
    private readonly IMinioClient _minioClient;
    private readonly Settings _settings;

    public FilesRepository(IMinioClient minioClient, Settings settings)
    {
        _minioClient = minioClient;
        _settings = settings;
    }

    /// <summary>
    /// Добавляет файл в хранилище.
    /// </summary>
    /// <param name="fileData">Содержимое файла.</param>
    /// <param name="fileExtension">Расширение файла.</param>
    /// <returns>Имя файла.</returns>
    public async Task<string> AddFileAsync(byte[] fileData, string fileExtension)
    {
        using var memoryStream = new MemoryStream(fileData);
        var fileName = $"{Guid.NewGuid():N}{fileExtension}";
        var putObjectArgs = new PutObjectArgs().WithBucket(_settings.FileStorageBucket)
            .WithStreamData(memoryStream)
            .WithObject(fileName).WithObjectSize(fileData.Length);
        await _minioClient.PutObjectAsync(putObjectArgs);


        return fileName;
    }

    /// <summary>
    /// Удаляет файл из хранилища.
    /// </summary>
    /// <param name="picture">Имя файла.</param>
    /// <returns>Задача.</returns>
    public async Task RemoveFileAsync(string picture)
    {
        await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
            .WithObject(picture)
            .WithBucket(_settings.FileStorageBucket));
    }

    public async Task<byte[]> GetFileAsync(string name)
    {
        using MemoryStream memoryStream = new MemoryStream();
         await _minioClient.GetObjectAsync(new GetObjectArgs()
            .WithBucket(_settings.FileStorageBucket)
            .WithObject(name).WithCallbackStream(stream=>stream.CopyTo(memoryStream)));
         return memoryStream.ToArray();
    }
}