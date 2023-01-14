using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoper.BusinessLogic.Interface;
using Shoper.Entities;
using X.PagedList;

namespace Shoper.UI.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;
        public CatalogController(IProductService productService)
        {
            _productService= productService;
        }
        

        [HttpGet]
        public IActionResult Index(int? page,int? pageSize,int? orderBy, int[]? category, int[]? manifacture,int? min,int? max)
        {           
            int _page=page.HasValue ? page.Value : 1;
            int _pageSize=pageSize.HasValue ? pageSize.Value : 2;
            ViewBag.page = _page;
            ViewBag.pageSize = _pageSize;
            ViewBag.orderBy = orderBy;
            ViewBag.Category = category;   // View tarafında sayfalamada url oluşturmak için
            ViewBag.Manifacture = manifacture;  //  ViewBag.Category = category;   // View tarafında sayfalamada url oluşturmak için
            ViewBag.min = min;
            ViewBag.max = max;
            var products = _productService.GetAll();
            // Kategori Koduna göre filtreleme
            if (category.Length > 0)
            {
               products = (from p in products join filtered in category on p.CategoryId equals filtered select p).ToList();
               ViewBag.filteredProducts = products; 
            }
            else
            {
                ViewBag.filteredProducts = null;
            }
            // Marka Koduna göre filtreleme
            if (manifacture.Length > 0)
            {
                products = (from p in products join filtered in manifacture on p.ManifactureId equals filtered select p).ToList();

            }
            // fiyatın max min değerine göre filtreleme
            if (min.HasValue && max.HasValue)
            {
                products = products.Where(x =>
                x.ProductPrice.FirstOrDefault(p => p.isValidFlag == true).Price >= min &&
                x.ProductPrice.FirstOrDefault(p => p.isValidFlag == true).Price <= max)
                .ToList();
            }
            if (orderBy.HasValue)
            {
                switch (orderBy)
                {
                    case 1:
                        {    // İsme göre sırala
                            products = products.OrderBy(x => x.ProductName);
                            break;
                        }
                    case 2:
                        {   // İsme göre azalan sırala
                            products = products.OrderByDescending(x => x.ProductName);
                            break;
                        }
                    case 3:
                        { // fiyata göre sırala
                            products = products.OrderBy(x => x.ProductPrice.FirstOrDefault(y=>y.isValidFlag==true).Price);
                            break;
                        }
                    case 4:
                        {   // fiyata göre azalan sırala
                            products = products.OrderByDescending(x => x.ProductPrice.FirstOrDefault(y => y.isValidFlag == true).Price);
                            break;
                        }
                    default:
                        break;
                }

            }

            return View(products.ToPagedList<Product>(_page,_pageSize));
        }
    
        public IActionResult ProductDetail(int id)
        {            
            var result = _productService.Get(id);            
            return View(result);
        }
        public bool TakeComment()
        {
            return true;
        }
        
    }
}
