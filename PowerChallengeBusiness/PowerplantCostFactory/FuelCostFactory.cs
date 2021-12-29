
namespace PowerChallengeBusiness
{
    public static class FuelCostFactory
    {
        public static T Create<T>() where T : IFuelCost, new()
        {
            return new T();
        }
        
    }
}
