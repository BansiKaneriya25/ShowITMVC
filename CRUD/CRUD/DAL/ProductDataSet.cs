using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CRUD.DAL
{
    public class ProductDataSet : DataSet
    {
        public DataTable Products { get; set; }

        public ProductDataSet()
        {
            Products = new DataTable("Products");
            // Define columns for the Products table
            Products.Columns.Add("Id", typeof(int));
            Products.Columns.Add("Name", typeof(string));
            Products.Columns.Add("Price", typeof(decimal));
        }
    }

    public static class ProductRepository
    {
        public static List<Product> ConvertDataSetToList(DataTable dataTable)
        {
            List<Product> productList = new List<Product>();
            
            foreach (DataRow row in dataTable.Rows)
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Price = Convert.ToDecimal(row["Price"])
                };
                productList.Add(product);
            }

            return productList;
        }
    }
}