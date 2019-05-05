using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Infrastructure.Shared.Enum
{
    public enum GenerateChangeStackModeEnum
    {
        /// <summary>
        /// Najpierw wszystkie elementy w jedym levelu, późmniej schodzi niżej
        /// </summary>
        Flat = 0,

        /// <summary>
        /// Od razu schodzi jak najniżej
        /// </summary>
        Deep = 1 ,

        /// <summary>
        /// Zamiast sprawdzac pojedyncze poleczenia sprawdza polaczenid do nakjblizszego
        /// </summary>
        NewMethod = 2,
    }
}
