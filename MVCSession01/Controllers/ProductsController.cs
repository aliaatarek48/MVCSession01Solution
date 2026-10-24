using Microsoft.AspNetCore.Mvc;
using MVCSession01.Models;

namespace MVCSession01.Controllers
{
    //[Route("Test")]
    public class ProductsController : Controller
    {
        #region Model Binding 

        // /Products/Search?Term=lap&MinPrice=100&Page=2 -> Term = lap , MinPrice = 100  , Page = 2 
        // /Products/Search?Term=lap&MinPrice=100 -> Term = lap , MinPrice = 100  , Page = 0 
        // /Products/Search?Term=lap -> Term = lap , MinPrice = null  , Page =0 
        // /Products/Search -> Term = null , MinPrice = null  , Page = 0 
        [HttpGet]
        public IActionResult Search(ProductFilter filter)
        {
            return View();
        }
        // GET BaseUrl/products/GetById              -> id = 0 , otherId = 0
        // GET BaseUrl/products/GetById/10           -> id = 0 , otherId = 0
        // GET BaseUrl/products/GetById?id=20        -> id = 20 , otherId = 0
        // GET BaseUrl/products/GetById?otherId=20   -> id = 0 , otherId = 0
        // GET BaseUrl/products/GetById?id2=20       -> id = 0 , otherId = 20
        [HttpGet]
        public IActionResult GetById([FromQuery] int id, [FromQuery(Name = "id2")] int otherId)
        {
            return Json(new { source = "Query", id, otherId });
        }
        #endregion

        #region Routing 
        [HttpGet("json")]      // GET BaseUrl/Test/json
        public IActionResult JsonAction()
        {
            return Json("Hello From Json Action");
        }

        [HttpGet("ProductCode/{code:int}")] // GET BaseUrl/test/status/200
        public IActionResult GetProductByCode(int code)
        {
            return Json(new Product { Id = code, Name = "Product01" });
        }

        #endregion

        #region Action and Action Result 
        // BaseUrl/Products
        [HttpGet]
        public IActionResult Index()
        {
            // Get All Products
            return View();
        }

        // BaseUrl/Products/GetProducts

        [HttpGet]
        public JsonResult GetProducts()
        {
            var products = new List<Product>()
            {
                new() { Id = 10, Name = "Laptop" },
                new() { Id = 20, Name = "Mobile" }
            };
            return Json(products);
        }


        // BaseUrl/Products/GetProductById/10
        [HttpGet]
        public IActionResult GetProductById(int Id)
        {
            if (Id < 0)
                return BadRequest();
            else if (Id == 0)
                return RedirectToAction(nameof(Index));
            else if (Id < 10)
                return NotFound();
            else
                return Json(new Product() { Id = Id, Name = "Laptop" });
        }


        // BaseUrl/Products/RedirectAction
        [HttpGet]
        public IActionResult RedirectAction()
        {
            return Redirect("https://docs.google.com/presentation/d/1TPKT3Esh3c8jtX1FJAifaLxjL2NlzjKM/edit?slide=id.p11#slide=id.p11");
        }

        // BaseUrl/Products/FileAction
        [HttpGet]
        public IActionResult FileAction()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes("wwwroot/files/LINQAssignment.pdf");
            return File(fileBytes, "application/pdf", "sample.pdf");
        }

        #endregion
    }
}
