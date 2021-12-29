using PowerChallengeBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerChallengeBusiness
{
    public class PowerLoadManager
    {
        public  PowerLoadManager(Payload payload, List<Power> resList)
        {
            this.payload = payload;
            this.resList = resList;

        }
        public  Power SetPower(PowerPlant pwrplant)
        {
            if (pwrplant.Pmax < payload.Load)
            {
                return SetNewPower(pwrplant.Name, pwrplant.Pmax);
            }
            else 
            {
                return CompareToPmin(pwrplant, payload.Load);
            }
        }
        public decimal RemovePwrPlant(Power producedPower, decimal load)
        {
            if (load > 0)load -= producedPower.PowerResult;
            if (Math.Round(load, 2) < 0)
            {
                var existingPwrPlant = resList.Select(x => x.PowerPlantName).Where(t => t.Contains(producedPower.PowerPlantName)).FirstOrDefault();
                if (existingPwrPlant != null)
                {
                    var pwrPlantToRemove = resList.Find(t => t.PowerPlantName == existingPwrPlant);
                    resList.Remove(pwrPlantToRemove);
                    load = payload.Load;
                }

            }
            return load;
        }
        public List<PowerPlant> SetOrder(List<PowerPlant> powerplantsList)
        {
            decimal payloadCount = powerplantsList.Sum(x => x.Pmax + x.Pmin);
            if (payload.Load <= payloadCount)
            {
                bool check = true;
                foreach (var plant in powerplantsList)
                {
                    decimal counter = plant.Pmin + plant.Pmax;
                    if (payload.Load >= counter)
                    {
                        check = false;

                    }
                }
                if (check)
                {
                    powerplantsList = powerplantsList.OrderBy(x => x.Pmin).ToList();
                }
            }
            return powerplantsList;
        }
        #region private
        private readonly Payload payload;
        private readonly List<Power> resList = new List<Power>();

        /// <summary>
        /// The power produced by each powerplant has to be a multiple of 0.1 Mw 
        /// </summary>
        /// <param name="power">produced power</param>
        /// <returns >closest to power(param) multiple of 0.1</returns>
        public decimal GetMultiple(decimal power)
        {
            decimal rem = power % (decimal)0.1;
            decimal multiple = power - rem;
            if (rem >= ((decimal)0.1 / 2))
                multiple += (decimal)0.1;
            return multiple;
        }
        private Power CompareToPmin(PowerPlant pwrplant, decimal load)
        {
            Power power;
            if (pwrplant.Pmin > load)
            {
                power = SetMinPower(pwrplant, load);
            }
            else
            {
                power = SetNewPower(pwrplant.Name, load);
            }
            return power;
        }

        private Power SetMinPower(PowerPlant pwrplant, decimal load)
        {
            Power power;
            if (resList.Any())
            {
                power = AdjustPrevious(pwrplant, load);
            }
            else
            {
                power = SetNewPower(pwrplant.Name, pwrplant.Pmin);
            }
            return power;
        }

        private Power AdjustPrevious(PowerPlant pwrplant, decimal load)
        {
            Power power;
            int count = resList.Count();
            resList.ElementAt(count - 1).PowerResult -= pwrplant.Pmin - load;
            power = SetNewPower(pwrplant.Name, pwrplant.Pmin);
            payload.Load = 0;
            return power;
        }

        private Power SetNewPower(string name, decimal powerRes)
        {
            return new Power()
            {
                PowerPlantName = name,
                PowerResult = powerRes
            };
        } 
        #endregion
    }
}
