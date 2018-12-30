using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vintereksamen_2017_2018_opgave_1
{
    public class ProductDTO
    {
        public ProductDTO(string productNumber, string name, string price)
        {
            ProductNumber = productNumber;
            Name = name;
            Price = price;
        }
        [JsonProperty(PropertyName = "id")]
        public string ProductNumber { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}
