using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IProductDiscountService _productDiscountService;
        private readonly IProductCommentService _productCommentService;
        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IProductImageService productImageService,
            IProductPriceService productPriceService,
            IProductDiscountService productDiscountService,
            IProductCommentService productCommentService
            )
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._productImageService = productImageService;
            this._productPriceService = productPriceService;
            this._productDiscountService= productDiscountService;
            this._productCommentService= productCommentService;
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
                    isValidFlag = true,
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
            model.Price = _productPriceService.GetExp(x => x.ProductId == id).FirstOrDefault(x=>x.isValidFlag==true);
            model.Images = _productImageService.GetExp(x => x.ProductId == id).ToList();
            
            ViewBag.Categories =  new SelectList(_categoryService.GetAll(),"CategoryId","CategoryName", model.Product.CategoryId);
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

               // model.Price.isValidFlag = true;
                //model.Price.ProductId = UpdatedModel.ProductId;
                ProductPrice priceModel = new ProductPrice()
                {
                    PriceId=model.Price.PriceId,
                    isValidFlag = true,
                    Price = model.Price.Price,
                    UnitPrice = UnitPrice,
                    ProductId = UpdatedModel.ProductId
                };
                _productPriceService.Update(priceModel);
                return RedirectToAction("Index");
            }
            else
                return View(model);

        }
        
        public bool Delete(int id)
        {
            var model = _productService.Get(id);
            return _productService.Delete(model) != null;
        }
        #region Comments
        public IActionResult Comments(int id)
        {
            return View(_productCommentService.GetExp(x=>x.ProductId==id));
        }
        #endregion
        #region Price
        public IActionResult PriceHistory(int id)
        {
            return View(_productPriceService.GetExp(x=>x.ProductId==id));
        }
        
        public IActionResult CreateNewPrice(int productId)
        {
            ProductPrice model = new ProductPrice();
            model.ProductId = productId;  // beelli olan bir ürüne yeni fiyat ekleme formu
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateNewPrice(ProductPrice model)
        {
            if (model != null)
            {
                model.isValidFlag = false;
                _productPriceService.Add(model);
                return RedirectToAction("PriceHistory", new {id=model.ProductId});
            }
            return View(model);
           
        }
        public bool ImageDelete(int imageId)
        {
            var result= _productImageService.Delete(_productImageService.Get(imageId));
            return result != null;
        }
        public bool SetValidFlag(int priceId)
        {
            ProductPrice priceModel = _productPriceService.Get(priceId);

            if (priceModel != null)
            {
                foreach (var item in _productPriceService.GetExp(x=>x.ProductId==priceModel.ProductId))
                {
                    item.isValidFlag = false;
                    _productPriceService.Update(item);
                }
                priceModel.isValidFlag = true;
                _productPriceService.Update(priceModel);
                return true;
            }
            return false;
        }
        #endregion
       
        #region discounts
        public IActionResult Discounts(int id)
        {
            ViewBag.productId = id;
            return View(_productDiscountService.GetExp(d=>d.ProductId==id));
        }
        public bool DeleteDiscount(int id)
        {
            var result = _productDiscountService.Delete(_productDiscountService.Get(id));
            return result != null;
        }
        public IActionResult CreateNewDiscount(int productId)
        {
            ProductDiscount model = new ProductDiscount();
            model.ProductId = productId;
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateNewDiscount(ProductDiscount model)
        {
            if (model != null)
            {
                _productDiscountService.Add(model);
                return RedirectToAction("Discounts", new {id=model.ProductId});
            }
            return View(model);
        }
        public IActionResult EditDiscount(int id)
        {
            var model=_productDiscountService.Get(id);
            return model != null ? View(model) : NotFound();
        }
        [HttpPost]
        public IActionResult EditDiscount(ProductDiscount model)
        {
            if (model!=null)
            {
               _productDiscountService.Update(model);
               return RedirectToAction("Discounts", new { id = model.ProductId });
            }
            return View(model);
        }
        #endregion
    }
}
