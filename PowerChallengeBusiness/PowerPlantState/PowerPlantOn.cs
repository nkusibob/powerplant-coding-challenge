using PowerChallengeBusiness.Models;
using System.Collections.Generic;
using System.Linq;

namespace PowerChallengeBusiness.PowerPlantState
{
    public class PowerPlantOn : State
    {
        private readonly Payload payload;
        private readonly Type type;
        private readonly List<Power> resList = new List<Power>();
        public PowerPlantOn(Payload payload, Type type) 
        {
            this.payload = payload;
            this.type = type;
        }
        public override List<Power> Handle(Context context)
        {
            int count = 0;
            PowerLoadManager powerLoadManager = new PowerLoadManager(payload, resList);
            List<PowerPlant> powerplantsList = payload.Powerplants.Where(x => x.Type == type).ToList();
            powerplantsList = powerLoadManager.SetOrder(powerplantsList);
            while ((payload.Load > 0) && (powerplantsList.Count - 1 >= count))
            {
                  Power power = powerLoadManager.SetPower(powerplantsList.ElementAt(count));
                  resList.Add(power);
                  payload.Load = powerLoadManager.RemovePwrPlant(power, payload.Load);
                  count++;
            }
            return resList;
        }

    }
}