using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;
using Shoper.BusinessLogic.Service;
using Shoper.Entities;
using System.Data;

namespace Shoper.Management.Controllers
{
	[Authorize(Roles = "manager")]
	public class ManifactureController : Controller
    {
        private readonly IManifactureService _manifactureService;
        public ManifactureController(IManifactureService manifactureService)
        {
            this._manifactureService = manifactureService;
        }
       

        public IActionResult Delete(int id)
        {
            _manifactureService.Delete(_manifactureService.Get(id));
            return Ok();
        }
        public IActionResult Index()
        {
            return View(_manifactureService.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Manifacture model, IFormFile img)
        {
            if (img != null)
            {
                string uzantı = Path.GetExtension(img.FileName);
                string resimAdi = model.ManifactureName + uzantı;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                model.ImagePath = resimAdi;
            }
            _manifactureService.Add(model);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            return View(_manifactureService.Get(id));
        }
        [HttpPost]
        public IActionResult Edit(Manifacture model, IFormFile img)
        {
            if (img != null)
            {
                string uzantı = Path.GetExtension(img.FileName);
                string resimAdi = model.ManifactureName + uzantı;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                model.ImagePath = resimAdi;
            }
            _manifactureService.Update(model);
            return RedirectToAction("Index");
        }
    }

}
