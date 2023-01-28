using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoper.BusinessLogic.Interface;
using Shoper.BusinessLogic.Service;
using Shoper.Entities;

namespace Shoper.Management.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ICategoryService _categoryService;
        public SliderController(ISliderService sliderService, ICategoryService categoryService)
        {
            _sliderService = sliderService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var result = _sliderService.GetAll();
            return View(result);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider model,IFormFile sliderImage)
        {
            if(model !=null)
            {
                if (sliderImage != null)
                {
                    byte[] data;
                    using (var br = new BinaryReader(sliderImage.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)sliderImage.OpenReadStream().Length);
                        model.sliderImage = data;
                    }
                }
                _sliderService.Add(model);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = new SelectList(_categoryService.GetAll(), "CategoryId", "CategoryName");
                return View();
            }
            
            
        }
        [HttpPost]
        public bool Delete(int id)
        {
            return _sliderService.Delete(_sliderService.Get(id)) != null;
        } 



    }
}
