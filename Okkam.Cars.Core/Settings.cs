using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okkam.Cars.Core
{
    /// <summary>
    /// Настройки.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Название бакета для хранения файлов.
        /// </summary>
        public string FileStorageBucket { get; set; } = null!;
    }
}
