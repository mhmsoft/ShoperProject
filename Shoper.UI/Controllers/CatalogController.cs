using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;

namespace Shoper.UI.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;
        public CatalogController(IProductService productService)
        {
            _productService= productService;
        }
        public IActionResult Index()
        {
            var products = _productService.GetAll();
            return View(products);
        }
    }
}
