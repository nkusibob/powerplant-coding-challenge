using PowerChallengeBusiness.Models;
using powerplant_coding_challenge.Mock;
using System;
using System.Collections.Generic;


namespace powerplant_coding_challenge.SwaggerRequest
{
    public class MockPayLoadExample3 : PowerPlantMock
    {
        public override List<PowerPlant> GetPowerPlants()
        {
            return base.GetPowerPlants();
        }
        public Payload GetExamples()
        {
            List<PowerPlant> PwrList = GetPowerPlants();
            return new Payload()
            {
                Load = 11,
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
