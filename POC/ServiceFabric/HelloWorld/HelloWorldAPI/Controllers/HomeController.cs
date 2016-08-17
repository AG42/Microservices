using HelloWorldAPI.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace HelloWorldAPI.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "Hello World !!!";
        }

        [HttpGet]
        [Route("products")]        
        public List<Product> GetAllProduct()
        {
            var products = new List<Product>();
            products.Add(new Product { Id = 1, Name = "Laptop", Code = "LP", Type = "111" });
            products.Add(new Product { Id = 2, Name = "Mobile", Code = "MB", Type = "222" });
            products.Add(new Product { Id = 3, Name = "iPad", Code = "iP", Type = "333" });
            products.Add(new Product { Id = 4, Name = "Tablet", Code = "TL", Type = "444" });
            products.Add(new Product { Id = 5, Name = "Desktop", Code = "DT", Type = "555" });

            return products;
        }

        [HttpGet]
        [Route("products/{productId}")]       
        public Product Product(int productId)
        {
            return GetAllProduct().Where(x => x.Id == productId).FirstOrDefault();
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
