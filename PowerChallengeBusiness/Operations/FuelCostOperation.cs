using PowerChallengeBusiness.Models;
using System;

namespace PowerChallengeBusiness.Operations
{
    public  class FuelCostOperation : IFuelCostOperation
    {
        public decimal GasCost(Fuels _fuel, PowerPlant _powerPlant) => (_fuel.Gas / _powerPlant.Efficiency) + (_fuel.Co2 * C02cost);
        public decimal KiroseneCost(Fuels _fuel, PowerPlant _powerPlant) => _fuel.Kerosine;

        private readonly decimal C02cost = Convert.ToDecimal(0.3);


    }
}
