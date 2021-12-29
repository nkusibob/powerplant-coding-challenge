using PowerChallengeBusiness.Models;
using PowerChallengeBusiness.Operations;


namespace PowerChallengeBusiness
{
    public interface IFuelCost
    {
        decimal GetPrice( FuelCostOperation fuelOp, PowerPlant pwrplant, Fuels fue);
        
    }
}
