
namespace Magisterka.Infrastructure.Shared.Common
{
    using Magisterka.Infrastructure.Shared.Structure;

    public class Settings
    {
        public BusStop StartBusStop { get; set; }

        public BusStop EndBusStop { get; set; }
        
        public double MutationProbability { get; set; }

        public  int InitialPopulationSize { get; set; }

        public int NumberOfAlgorithmIterations { get; set; }

        public int MaximumNumberOfChanges { get; set; }
    }
}