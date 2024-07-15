using CRUD.DAL;
using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ShowITConnectionString"].ConnectionString;

        // GET: Product
        public ActionResult Index()
        {
            var list = new List<Product>();
            var dataSet = new ProductDataSet();

            using (var connection = new SqlConnection(connectionString))
            {
                // this is inline query
                //using (var command = new SqlCommand("SELECT * FROM Products", connection))
                //{
                //    command.CommandType = CommandType.Text;
                //    connection.Open();
                //    using (var reader = command.ExecuteReader())
                //    {
                //        dataSet.Products.Load(reader);
                //    }
                //}
                using (var command = new SqlCommand("GetProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dataSet.Products.Load(reader);
                    }
                }
            }
            list = ProductRepository.ConvertDataSetToList(dataSet.Products);
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            int rowsaffected;
            bool IsSaved = false;
            string msg = "";
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("SaveProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    connection.Open();
                    rowsaffected = command.ExecuteNonQuery();
                }
            }
            if (rowsaffected > 0)
            {
                IsSaved = true;
                msg = "Data is saved successfully.";
            }
            else
            {
                IsSaved = false;
                msg = "Data saved failed";
            }
            //return RedirectToAction("Index");
            return Json(new { isSaved = IsSaved, Message = msg });
        }

        public ActionResult Edit(int id)
        {
            Product product = new Product();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetProductById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product.Id = (int)reader["Id"];
                            product.Name = reader["Name"].ToString();
                            product.Price = (decimal)reader["Price"];
                        }
                    }
                }
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("SaveProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    connection.Open();
                    int rowsaffected = command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("DeleteProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

    }
}