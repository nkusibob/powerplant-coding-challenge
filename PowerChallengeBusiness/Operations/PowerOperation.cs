using PowerChallengeBusiness.Models;
using System.Collections.Generic;
using System.Linq;


namespace PowerChallengeBusiness.Operations
{
    public class PowerOperation
    {
        List<Power> resList = new List<Power>();
        PowerPlantTypeManager PlantTypeManager;
        public List<Power> GetPower(Payload payload, FuelCostOperation _fuelOp)
        {
            if (payload?.Fuels?.Kerosine <= 0)
                throw new System.NullReferenceException("No cost for Kerosine was found");
            if (payload?.Fuels?.Gas <= 0)
                throw new System.NullReferenceException("No price for Gas was found");
            if (payload?.Load <= 0)
                throw new System.NullReferenceException("No Load  was found");
            // allow to test the 3 given payload 
            //using unitest
            if (resList.Any()) resList.Clear();
            if (payload != null)
            {
                PowerLoadManager powerLoadManager = new PowerLoadManager(payload,resList);
                GetAllTypePower(payload, _fuelOp);
                resList.ForEach(x => x.PowerResult = powerLoadManager.GetMultiple(x.PowerResult));
            }
            return resList.Where(x => x.PowerResult > 0).ToList();
        }
       
        #region private methods
        private void GetAllTypePower(Payload payload, FuelCostOperation _fuelOp)
        {
            PlantTypeManager = new PowerPlantTypeManager();
            if (!payload.Powerplants.Any())
                throw new System.NullReferenceException("no powerplant found !");
            resList = PlantTypeManager.GetWindPower(payload, resList);
            PowerCostManager powerCostManager = new PowerCostManager(PlantTypeManager);
            Type CheapCostkey = powerCostManager.GetCheapestPowerPlant(payload, _fuelOp);
            if (CheapCostkey == Type.gasfired)
            {
                resList = powerCostManager.GazFirst(payload,resList);
            }
            else
            {
                resList = powerCostManager.TurboJetFirst(payload,resList) ;
            }

        }
        
    }
}
#endregion

