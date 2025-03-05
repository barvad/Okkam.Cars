using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Okkam.Cars.Core.Contracts;
using Okkam.Cars.Core.Entities;
using Okkam.Cars.WebApi.ViewModels;

namespace Okkam.Cars.WebApi.Controllers;

/// <summary>
/// Контроллер автомобилей.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarsService _carsService;

    private readonly ILogger _logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="loggerFactory">Фабрика для создания логгера.</param>
    /// <param name="carsService">Сервис автомобилей.</param>
    public CarsController(ILoggerFactory loggerFactory, ICarsService carsService)
    {
        _carsService = carsService;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    /// <summary>
    /// Добавляет автомобиль.
    /// </summary>
    /// <param name="carViewModel">Модель представления автомобиля.</param>
    /// <returns>Результат выполнения метода.</returns>
    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult> Add(CarAddViewModel carViewModel)
    {
        var car = new Car
        {
            BodyStyle = new BodyStyle
            {
                Id = carViewModel.BodyStyleId
            },
            Brand = new Brand
            {
                Id = carViewModel.BrandId
            },
            Name = carViewModel.Name,
            Picture = carViewModel.Picture.FileName,
            SeatsCount = carViewModel.SeatsCount,
            Url = carViewModel.Url
        };

        using var memoryStream = new MemoryStream();
        carViewModel.Picture.OpenReadStream().CopyTo(memoryStream);
        if (!await _carsService.AddCarAsync(car, memoryStream.ToArray())) return Conflict();

        return Ok();
    }

    /// <summary>
    /// Возвращает постраничное представление для автомобилей.
    /// </summary>
    /// <param name="page">Страница.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Постраничное представление для автомобилей.</returns>
    [HttpGet]
    [Route("[action]/{page}")]
    public async Task<CarsPagingViewModel> GetList(int page = 1, int pageSize = 10)
    {
        var (count, cars) = await _carsService.GetList(page, pageSize);
        return new CarsPagingViewModel
        {
            PageNumber = page,
            PageSize = pageSize,
            TotalCount = count,
            TotalPages = count / pageSize
                         + (count % pageSize == 0 ? 0 : 1),
            Cars = cars.Select(ConvertToListItemViewModel)
        };
    }

    /// <summary>
    /// Возвращает автомобиль для редактирования.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Автомобиль для редактирования.</returns>
    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<CarViewModel> GetCar(int id)
    {
        var car = await _carsService.GetCarAsync(id);
        return ConvertToViewModel(car);
    }

    /// <summary>
    /// Возвращает изображение автомобиля.
    /// </summary>
    /// <param name="fileName">Имя файла изображения.</param>
    /// <returns>Изображение автомобиля.</returns>
    [HttpGet]
    [Route("[action]/{fileName}")]
    public async Task<FileResult> GetPicture(string fileName)
    {
        var buffer = await _carsService.GetFileAsync(fileName);
        var fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
        fileExtensionContentTypeProvider.TryGetContentType(fileName, out var contentType);
        return File(buffer, contentType);
    }


    /// <summary>
    /// Редактирует автомобиль.
    /// </summary>
    /// <param name="carViewModel">Модель представления автомобиля.</param>
    /// <returns>Результат выполнения метода.</returns>
    [HttpPut]
    [Route("[action]")]
    public async Task<ActionResult> Edit([FromForm] CarEditViewModel carViewModel)
    {
        var car = Convert(carViewModel);
        if (carViewModel.Picture == null) 
            return await EditCarAsync(car, null!);
        using var memoryStream = new MemoryStream();
        await carViewModel.Picture.OpenReadStream().CopyToAsync(memoryStream);
        return await EditCarAsync(car, memoryStream.ToArray());

        return Ok();
    }


    /// <summary>
    /// Редактирует автомобиль.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Результат выполнения метода.</returns>
    [HttpDelete]
    [Route("[action]/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _carsService.DeleteCarAsync(id);
        return Ok();
    }


    private async Task<ActionResult> EditCarAsync(Car car, byte[] pictureData)
    {
        if (!await _carsService.EditCarAsync(car, pictureData)) return Conflict();
        return Ok();
    }
    private static Car Convert(CarEditViewModel carViewModel)
    {
        return new Car
        {
            Id = carViewModel.Id,
            BodyStyle = new BodyStyle
            {
                Id = carViewModel.BodyStyleId
            },
            Brand = new Brand
            {
                Id = carViewModel.BrandId
            },
            Name = carViewModel.Name,
            Picture = carViewModel.Picture?.FileName!,
            SeatsCount = carViewModel.SeatsCount,
            Url = carViewModel.Url
        };
    }

    private CarListItemViewModel ConvertToListItemViewModel(Car car)
    {
        return new CarListItemViewModel
        {
            Id = car.Id,
            BodyStyle = car.BodyStyle.Name,
            Brand = car.Brand.Name,
            Name = car.Name,
            SeatsCount = car.SeatsCount
        };
    }

    private CarViewModel ConvertToViewModel(Car car)
    {
        return new CarViewModel
        {
            Id = car.Id,
            Name = car.Name,
            BodyStyleId = car.BodyStyle.Id,
            BrandId = car.Brand.Id,
            PictureName = car.Picture,
            SeatsCount = car.SeatsCount,
            Url = car.Url
        };
    }
}