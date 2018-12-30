using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vintereksamen_2017_2018_opgave_1
{
    public class ProductForOrder
    {
        public ProductForOrder(int count, string name, string price)
        {
            Count = count;
            Name = name;
            Price = price;
        }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int TotalPrice
        {
            get
            {
                return Count * System.Convert.ToInt32(Price);
            }
        } 
    }
}
