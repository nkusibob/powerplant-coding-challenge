using PowerChallengeBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powerplant_coding_challenge.Mock
{
    public class PowerPlantMock
    {
        List<PowerPlant> PwrList = new List<PowerPlant>();
        PowerPlant powerplant1 = new PowerPlant()
        {
            Name = "gasfiredbig1",
            Type = PowerChallengeBusiness.Models.Type.gasfired,
            Efficiency = Convert.ToDecimal(0.53),
            Pmin = 100,
            Pmax = 460

        };
        PowerPlant powerplant2 = new PowerPlant()
        {
            Name = "gasfiredbig2",
            Type = PowerChallengeBusiness.Models.Type.gasfired,
            Efficiency = Convert.ToDecimal(0.53),
            Pmin = 100,
            Pmax = 460

        };
        PowerPlant powerplant3 = new PowerPlant()
        {
            Name = "gasfiredsomewhatsmaller",
            Type = PowerChallengeBusiness.Models.Type.gasfired,
            Efficiency = Convert.ToDecimal(0.37),
            Pmin = 40,
            Pmax = 210

        };
        PowerPlant powerplant4 = new PowerPlant()
        {
            Name = "tj1",
            Type = PowerChallengeBusiness.Models.Type.turbojet,
            Efficiency = Convert.ToDecimal(0.3),
            Pmin = 0,
            Pmax = 16

        };
        PowerPlant powerplant5 = new PowerPlant()
        {
            Name = "windpark1",
            Type = PowerChallengeBusiness.Models.Type.windturbine,
            Efficiency = Convert.ToDecimal(1),
            Pmax = 150,
            Pmin = 0
        };
        PowerPlant powerplant6 = new PowerPlant()
        {
            Name = "windpark2",
            Type = PowerChallengeBusiness.Models.Type.windturbine,
            Efficiency = Convert.ToDecimal(1),
            Pmax = 36,
            Pmin = 0
        };
        public virtual List<PowerPlant> GetPowerPlants()
        {
            PwrList.Add(powerplant1);
            PwrList.Add(powerplant2);
            PwrList.Add(powerplant3);
            PwrList.Add(powerplant4);
            PwrList.Add(powerplant5);
            PwrList.Add(powerplant6);
            return PwrList;

        }
    }
}
