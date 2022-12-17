using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;
using Shoper.Entities;

namespace Shoper.Management.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductItemService _itemService;
        public CategoryController(
            ICategoryService categoryService,
            IProductItemService itemService
            )
        {
            this._categoryService = categoryService;
            this._itemService = itemService;
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
        #region Items
        public IActionResult Items(int id)
        {
            ViewBag.Id = id;
            return View(_itemService.GetExp(x => x.CategoryId == id));
        }
        public bool DeleteItem(int id)
        {
           return _itemService.Delete(_itemService.Get(id))!=null;
        }

        public IActionResult CreateItem(int id)
        {
            ProductItem newItem = new ProductItem()
            {
                CategoryId = id
            };
            return View(newItem);
        }
        [HttpPost]
        public IActionResult CreateItem(ProductItem model)
        {
            if (model != null)
            {
                var result=_itemService.Add(model);
                return RedirectToAction("Items", new { id = result.CategoryId });
            }
            return View(model);
        }
        public IActionResult EditItem(int id)
        {
            return View(_itemService.Get(id));
        }
        [HttpPost]
        public IActionResult EditItem(ProductItem model)
        {
            if (model != null)
            {
                var result = _itemService.Update(model);
                return RedirectToAction("Items", new { id = result.CategoryId });
            }
            return View(model);
        }

        #endregion
    }
}
