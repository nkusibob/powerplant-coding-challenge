using PowerChallengeBusiness.Models;
using PowerChallengeBusiness.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Type = PowerChallengeBusiness.Models.Type;

namespace PowerChallengeBusiness
{
    public class PowerCostManager
    {
        public PowerCostManager(PowerPlantTypeManager plantTypeManager)
        {
            this.PlantTypeManager = plantTypeManager;
        }
        public Type GetCheapestPowerPlant(Payload payload, FuelCostOperation _fuelOp)
        {
            Dictionary<Type, decimal> costByFuelType = new Dictionary<Models.Type, decimal>();
            Type CheapCostkey;
            try
            {
                foreach (PowerPlant pwrplant in payload.Powerplants)
                {
                    if (pwrplant.Efficiency <= 0)
                        throw new NullReferenceException(string.Format("no efficiency found for {0} ", pwrplant.Name));
                    if (pwrplant.Pmax <= 0)
                        throw new NullReferenceException(string.Format("no Valid Pmax found for {0} ", pwrplant.Name));
                    if (pwrplant.Type != Type.windturbine)
                    {
                        decimal costByType = CompareCost(pwrplant,_fuelOp,payload);
                        if (!costByFuelType.ContainsKey(pwrplant.Type))
                            costByFuelType.Add(pwrplant.Type, costByType);
                    }
                }
                CheapCostkey = costByFuelType.OrderBy(kvp => kvp.Value).First().Key;
            }
            catch (Exception)
            {
                throw;
            }
            return CheapCostkey;

        }
        public List<Power> GazFirst(Payload payload, List<Power> resList)
        {
            resList = PlantTypeManager.GetGazPower(payload, resList);
            resList = PlantTypeManager.GetTurboJetPower(payload, resList);
            return resList;
        }
        public List<Power> TurboJetFirst(Payload payload, List<Power> resList)
        {
            resList = PlantTypeManager.GetTurboJetPower(payload, resList);
            resList = PlantTypeManager.GetGazPower(payload, resList);
            return resList;
        }
        #region private
        private readonly PowerPlantTypeManager PlantTypeManager;
        private decimal CompareCost(PowerPlant pwrplant, FuelCostOperation _fuelOp, Payload payload)
        {
            IFuelCost powerplant;
            if (pwrplant.Type == Type.gasfired)
            {
                powerplant = FuelCostFactory.Create<GasCost>();
            }
            else
            {
                powerplant = FuelCostFactory.Create<KiroseneCost>();
            }
            return powerplant.GetPrice(_fuelOp, pwrplant, payload?.Fuels);
        } 
        #endregion
    }
}
