using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Infrastructure.Shared.Enum
{
    /// <summary>
    /// Sposób w jaki lista przystanków zosdtanie połączona w trase
    /// </summary>
    public enum GeneratorConnectionTypeEnum
    {
        /// <summary>
        /// Połączone zostana bezpośrednio
        /// </summary>
        ConnectDirectly = 0,

        /// <summary>
        /// Przystanki to przesiadki, musi znaleść linie
        /// </summary>
        FindMisssingLines =1
    }
}
