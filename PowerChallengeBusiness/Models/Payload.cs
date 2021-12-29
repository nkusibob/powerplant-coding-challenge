using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PowerChallengeBusiness.Models
{
    public  class Payload
    {
        public Payload()
        {
            Powerplants = new HashSet<PowerPlant>();     
        }
        public decimal Load { get; set; }
        public  Fuels Fuels { get; set; }
        public virtual ICollection<PowerPlant> Powerplants { get; set; }


    }
}
 