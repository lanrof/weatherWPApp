using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Model.SearchResponse
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "sys")]
        public AdditionalInfo AdditionalInfo { get; set; }
    }
}
