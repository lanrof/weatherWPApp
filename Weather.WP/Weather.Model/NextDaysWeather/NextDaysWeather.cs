using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Model.NextDaysWeather
{
    public class NextDaysWeather
    {
        [JsonProperty(PropertyName = "dt")]
        public int DayTimestamp { get; set; }

        [JsonProperty(PropertyName = "temp")]
        public Temperatures Temperatures { get; set; }

        public double Pressure { get; set; }

        public int Humidity { get; set; }

        public List<WeatherResponse.Weather> Weather { get; set; }
    }
}
