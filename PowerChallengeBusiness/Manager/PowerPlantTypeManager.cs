using PowerChallengeBusiness.Models;
using PowerChallengeBusiness.PowerPlantState;
using System.Collections.Generic;

namespace PowerChallengeBusiness
{
    public class PowerPlantTypeManager
    {
        public List<Power> GetWindPower(Payload payload,List<Power> resList)
        {
            if (payload.Fuels?.Wind > 0)
            {
                var context = new Context(new PowerPlantOn(payload, Type.windturbine));
                resList = context.Request();
            }
            return resList;
        }
        public List<Power> GetTurboJetPower(Payload payload, List<Power> resList)
        {
            if (payload.Load > 0)
            {
                var contextTurbojet = new Context(new PowerPlantOn(payload, Type.turbojet));
                resList.AddRange(contextTurbojet.Request());
            }
            return resList;
        }
        public List<Power> GetGazPower(Payload payload, List<Power> resList)
        {
            if (payload.Load > 0)
            {
               var contextgasfired = new Context(new PowerPlantOn(payload, Type.gasfired));
                resList.AddRange(contextgasfired.Request());
            }
            return resList;
        }

    }
}