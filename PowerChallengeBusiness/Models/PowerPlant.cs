using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PowerChallengeBusiness.Models
{
    public class PowerPlant
    {
        public string Name  { get; set; }
        [EnumDataType(typeof(Type))]
        [JsonConverter(typeof(StringEnumConverter))]
        public Type Type { get; set; }
        public decimal Efficiency { get; set; }
        public decimal Pmax { get; set; }
        public decimal Pmin { get; set; }



    }
}