using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;
using Shoper.Entities;
using Shoper.Management.Models.ViewModels;

namespace Shoper.Management.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;
        private readonly IProductPriceService _productPriceService;
        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IProductImageService productImageService,
            IProductPriceService productPriceService
            )
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._productImageService = productImageService;
            this._productPriceService = productPriceService;
        }
        public IActionResult Index()
        {
            return View(_productService.GetAll());
        }
        public IActionResult Create()
        {
            ViewData["categories"] = _categoryService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product model,IEnumerable<IFormFile> img,decimal price,UnitPrice UnitPrice)
        {
            if (model != null)
            {
                var InsertedModel = _productService.Add(model);
                if (img.Count() > 0)
                {
                    byte[] data;
                    foreach (var item in img)
                    {
                        ProductImage imageModel = new ProductImage();
                        imageModel.ProductId = InsertedModel.ProductId;
                        using (var br = new BinaryReader(item.OpenReadStream()))
                        {
                            data = br.ReadBytes((int)item.OpenReadStream().Length);
                            imageModel.Image = data;
                        }
                        _productImageService.Add(imageModel);
                    }
                }
                ProductPrice priceModel = new ProductPrice()
                {
                    isValidFlag = false,
                    Price = price,
                    UnitPrice = UnitPrice,
                    ProductId = InsertedModel.ProductId
                };
                _productPriceService.Add(priceModel);
                return RedirectToAction("Index");
            }
            else
                return View(model);
            
        }
        public IActionResult Edit(int id)
        {
            Product_Price_Img_VM model = new Product_Price_Img_VM();
            model.Product=_productService.Get(id);
            model.Prices = _productPriceService.GetExp(x => x.ProductId == id).ToList();
            model.Images = _productImageService.GetExp(x => x.ProductId == id).ToList();
            ViewData["categories"] = _categoryService.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Product_Price_Img_VM model, IEnumerable<IFormFile> img, decimal price, UnitPrice UnitPrice)
        {
            if (model != null)
            {
                var UpdatedModel = _productService.Update(model.Product);
                if (img.Count() > 0)
                {
                    byte[] data;
                    foreach (var item in img)
                    {
                        ProductImage imageModel = new ProductImage();
                        imageModel.ProductId = UpdatedModel.ProductId;
                        using (var br = new BinaryReader(item.OpenReadStream()))
                        {
                            data = br.ReadBytes((int)item.OpenReadStream().Length);
                            imageModel.Image = data;
                        }
                        _productImageService.Add(imageModel);
                    }
                }
                ProductPrice priceModel = new ProductPrice()
                {
                    isValidFlag = model.Prices.FirstOrDefault().isValidFlag?true:false,
                    Price = price,
                    UnitPrice = UnitPrice,
                    ProductId = UpdatedModel.ProductId
                };
                _productPriceService.Update(priceModel);
                return RedirectToAction("Index");
            }
            else
                return View(model);

        }
    }
}
