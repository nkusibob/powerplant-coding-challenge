using Microsoft.Extensions.DependencyInjection;
using PowerChallengeBusiness.Models;

namespace PowerChallengeBusiness
{
    public interface IFuelCostOperation
    {
        public decimal GasCost(Fuels _fuel, PowerPlant _powerPlant);
        public decimal KiroseneCost(Fuels _fuel, PowerPlant _powerPlant);

    }
}


