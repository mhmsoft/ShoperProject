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
        public IActionResult Index(int? page,int? pageSize,int? orderBy, int[]? category)
        {
           
            int _page=page.HasValue ? page.Value : 1;
            int _pageSize=pageSize.HasValue ? pageSize.Value : 2;
            ViewBag.page = _page;
            ViewBag.pageSize = _pageSize;
            if (category.Length > 0)
            {

                
            }
           var products = _productService.GetAll();
            if (orderBy.HasValue)
            {
                switch (orderBy)
                {
                    case 1:
                        {
                            products = products.OrderBy(x => x.ProductName);
                            break;
                        }
                    case 2:
                        {
                            products = products.OrderByDescending(x => x.ProductName);
                            break;
                        }
                    case 3:
                        {
                            products = products.OrderBy(x => x.ProductPrice.FirstOrDefault(y=>y.isValidFlag==true).Price);
                            break;
                        }
                    case 4:
                        {
                            products = products.OrderByDescending(x => x.ProductPrice.FirstOrDefault(y => y.isValidFlag == true).Price);
                            break;
                        }
                    default:
                        break;
                }

            }

            return View(products.ToPagedList<Product>(_page,_pageSize));
        }
    }
}
