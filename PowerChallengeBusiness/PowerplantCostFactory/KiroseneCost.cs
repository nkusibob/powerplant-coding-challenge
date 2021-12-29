using PowerChallengeBusiness.Models;
using System;


namespace PowerChallengeBusiness
{
    public  class KiroseneCost : IFuelCost
    {
        public decimal GetPrice(Operations.FuelCostOperation fuelOp, PowerPlant powerPlants, Fuels fuel)
        {
            if (fuel == null)
            {
                throw new NullReferenceException(" there isn't any type of fuel");
            }
            return fuelOp.KiroseneCost(fuel,powerPlants);
        }
    }
}
