﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Model.WeatherResponse
{
    public class Clouds
    {
        [JsonProperty(PropertyName = "all")]
        public double CloudinessInPercents { get; set; }
    }
}
