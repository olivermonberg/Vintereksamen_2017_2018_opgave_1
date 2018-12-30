using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProductDB.Models
{
    public class Product
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string ProductNumber { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}
