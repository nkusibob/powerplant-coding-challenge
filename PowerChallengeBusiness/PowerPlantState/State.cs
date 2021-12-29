using PowerChallengeBusiness.Models;
using System.Collections.Generic;

namespace PowerChallengeBusiness.PowerPlantState
{
    public abstract class State
    {
        public abstract List<Power> Handle(Context context);
    }
}
