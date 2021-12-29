using PowerChallengeBusiness.Models;
using powerplant_coding_challenge.Mock;
using System;
using System.Collections.Generic;


namespace powerplant_coding_challenge.SwaggerRequest
{
    public class MockPayLoadExample4 : PowerPlantMock
    {
        readonly List<PowerPlant> PwrList = new List<PowerPlant>();
        readonly PowerPlant powerplant1 = new PowerPlant()
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
       
        public override List<PowerPlant> GetPowerPlants()
        {
            PwrList.Add(powerplant1);
            PwrList.Add(powerplant2);
            return PwrList;
        }
        public Payload GetExamples()
        {
            List<PowerPlant> PwrList = GetPowerPlants();
            return new Payload()
            {
                Load = 500,
                Fuels = new Fuels()
                {
                    Gas = Convert.ToDecimal(13.4),
                    Kerosine = Convert.ToDecimal(50.8),
                    Co2 = Convert.ToDecimal(20),
                    Wind = 0

                },
                Powerplants = PwrList
            };
        }
    }
}
