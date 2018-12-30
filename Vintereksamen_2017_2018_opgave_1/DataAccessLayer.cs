using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Vintereksamen_2017_2018_opgave_1
{
    public class DataAccessLayer
    {
        private HttpClient client = new HttpClient();
        Uri _uri = new Uri("http://localhost:49651");

        public DataAccessLayer()
        {
            client.BaseAddress = _uri;
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var str = await client.GetStringAsync($"api/Products");
            var toReturn = JsonConvert.DeserializeObject<List<ProductDTO>>(str);
            return toReturn;
        }

        public async Task<bool> Add_Product(ProductDTO _product)
        {
            var str = await client.PostAsJsonAsync($"api/Products", _product);
           
            if (str.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProduct(ProductDTO _product)
        {
            var str = await client.PutAsJsonAsync($"api/Products/{_product.ProductNumber}", _product);

            if (str.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var str = await client.DeleteAsync($"api/Products/{productId}");

            if (str.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
    }
}
