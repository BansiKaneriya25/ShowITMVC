using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Web_API_Auth.Controllers
{
    [RoutePrefix("api/Product")]
    [Authorize(Roles = "Admin,Product")]
    public class ProductController : ApiController
    {
        ShowDotNetITEntities DB = new ShowDotNetITEntities();

        [Route("GetProducts")]
        [HttpGet]
        public List<Product> GetProducts()
        {
            return DB.Products.ToList();
        }

        [HttpGet]
        [Route("GetProductByName/{productName}")] //Atribute Based Routing
        public Product GetProductByName(string productName)
        {
            return DB.Products.Where(x => x.Name.Equals(productName)).FirstOrDefault();
        }

        [HttpGet]
        [Route("GetProductByLikeName/{productName}")] //Atribute Based Routing
        public List<Product> GetProductByLikeName(string productName)
        {
            return DB.Products.Where(x => x.Name.Contains(productName)).ToList();
        }

        [HttpPost]
        [Route("CreateProduct")] //Atribute Based Routing
        public string CreateProduct(Product model)
        {
            if (model != null)
            {
                DB.Products.Add(model);
                DB.SaveChanges();

                return "Product has been added.";
            }
            else
            {
                return "";
            }
        }

    }
}