using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;
using Shoper.Entities;

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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category model,IFormFile img)
        {
            if (img != null)
            {
                string uzantı = Path.GetExtension(img.FileName);
                string resimAdi = model.CategoryName  + uzantı;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                model.CategoryImagePath = resimAdi;
            }
            _categoryService.Add(model);
            return RedirectToAction("Index");            
        }
        public IActionResult Edit(int id)
        {
            return View(_categoryService.Get(id));
        }
        [HttpPost]
        public IActionResult Edit(Category model,IFormFile img)
        {
            if (img != null)
            {
                string uzantı = Path.GetExtension(img.FileName);
                string resimAdi = model.CategoryName + uzantı;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                model.CategoryImagePath = resimAdi;
            }
            _categoryService.Update(model);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(_categoryService.Get(id));
            return Ok();
        }
    }
}
