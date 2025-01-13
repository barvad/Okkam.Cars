using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okkam.Cars.Core.Entities
{
    public class Car
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public int Id { get; set; }

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
        public BodyStyle BodyStyle { get; set; }

        /// <summary>
        /// Бренд.
        /// </summary>
        public Brand Brand { get; set; }
    }
}
