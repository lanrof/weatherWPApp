using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Model.WeatherResponse
{
    public class Wind
    {
        public double Speed { get; set; }
        [JsonProperty(PropertyName = "deg")]
        public double DirectionInDeg { get; set; }
    }
}
