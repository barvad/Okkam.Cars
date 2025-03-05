using System.Runtime.Serialization;

namespace Okkam.Cars.Ef.Entities;

public class CarEntity
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Бренд.
    /// </summary>
    public int BrandId { get; set; }

    /// <summary>
    /// Название модели.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Изображение.
    /// </summary>
    public string Picture { get; set; }

    /// <summary>
    /// Дата и время создания записи в БД.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Идентификатор типа кузова.
    /// </summary>
    public int BodyStyleId { get; set; }

    /// <summary>
    /// Число сидений в салоне.
    /// </summary>
    public byte SeatsCount { get; set; }

    /// <summary>
    /// Url сайта официального дилера.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Тим кузова.
    /// </summary>
    [IgnoreDataMember]
    public virtual BodyStyleEntity BodyStyle { get; set; }

    /// <summary>
    /// Бренд.
    /// </summary>
    [IgnoreDataMember]
    public virtual BrandEntity Brand { get; set; }

}