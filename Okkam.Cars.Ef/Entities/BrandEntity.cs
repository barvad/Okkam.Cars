using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okkam.Cars.Ef.Entities
{
    /// <summary>
    /// Бренд.
    /// </summary>
    public class BrandEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название бренда.
        /// </summary>
        public string Name { get; set; }
    }
}
