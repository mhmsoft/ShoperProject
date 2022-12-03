using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;

namespace Shoper.Management.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
       
        public IActionResult Index()
        {
            return View(_categoryService.GetAll());
        }
    }
}
