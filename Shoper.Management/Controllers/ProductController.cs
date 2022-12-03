using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;

namespace Shoper.Management.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        public IActionResult Index()
        {
            return View(_productService.GetAll());
        }
    }
}
