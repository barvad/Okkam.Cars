using Microsoft.AspNetCore.Mvc;
using Okkam.Cars.Core.Contracts;
using Okkam.Cars.Core.Entities;

namespace Okkam.Cars.WebApi.Controllers;

/// <summary>
/// Контроллер фильтров.
/// </summary>
[ApiController]
[Route("[controller]")]
public class FiltersController : ControllerBase
{
    private readonly IFiltersService _filtersService;

    private readonly ILogger _logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="loggerFactory">Фабрика для создания логгера.</param>
    /// <param name="filtersService"></param>
    public FiltersController(ILoggerFactory loggerFactory, IFiltersService filtersService)
    {
        _filtersService = filtersService;
        _logger = loggerFactory.CreateLogger(GetType());
    }


    /// <summary>
    /// Возвращает фильтры по брэнду.
    /// </summary>
    /// <returns>Фильтры по брэнду.</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<Brand[]> GetBrands()
    {
        return await _filtersService.GetBrandsAsync();
    }

    /// <summary>
    /// Возвращает фильтры по брэнду.
    /// </summary>
    /// <returns>Фильтры по брэнду.</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<BodyStyle[]> GetBodyStyles()
    {
        return await _filtersService.GetBodyStylesAsync();
    }
}