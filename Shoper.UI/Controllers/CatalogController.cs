using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

using Shoper.BusinessLogic.Interface;
using Shoper.Entities;
using Shoper.UI.Models.ViewModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using X.PagedList;



namespace Shoper.UI.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCommentService _productCommentService;
        public CatalogController(IProductService productService, IProductCommentService productCommentService)
        {
            _productService= productService;
            _productCommentService= productCommentService;
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

        [HttpPost]
        public IActionResult TakeComment(string comment,string name,string email,int productId)
        {
            
            ProductComment productComment = new ProductComment();
            productComment.Comment = comment;
            productComment.CommentDate = DateTime.Now;
            productComment.Writer = name;
            productComment.Email = email;
            productComment.ProductId = productId;
            _productCommentService.Add(productComment);
            return RedirectToAction("ProductDetail", new {id=productId});
        }
        private int isExistInCard(int id)
        {
            var cardSession = HttpContext.Session.GetString("Card");
            List<CardItem> card = JsonSerializer.Deserialize<List<CardItem>>(cardSession);
            for (int i = 0; i < card.Count; i++)
                if (card[i].product.ProductId.Equals(id))
                    return i;
            return -1;
        }
        [HttpPost]
        public bool Remove(int productId)
        {
            var cardSession = HttpContext.Session.GetString("Card");
            List<CardItem> card = JsonSerializer.Deserialize<List<CardItem>>(cardSession);
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            if (card.Exists(x => x.product.ProductId == productId))
            {
                int index = isExistInCard(productId);
                card.RemoveAt(index);
                HttpContext.Session.SetString("Card", JsonSerializer.Serialize(card, options));
                return true;

            }
            return false;

        }
        public IActionResult AddCard(int productId,int quantity)
        {
            Product product = _productService.Get(productId);
            List<CardItem> card = new List<CardItem>();
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            if (HttpContext.Session.GetString("Card")==null)
            {
                
                card.Add(new CardItem() {Id=Guid.NewGuid(), product=product, quantity=quantity});
                HttpContext.Session.SetString("Card", JsonSerializer.Serialize(card, options));
            }
            else
            {
                var cardSession = HttpContext.Session.GetString("Card");                  
                 card = JsonSerializer.Deserialize<List<CardItem>>(cardSession);
                // sepette eklenen ürünün  sepetteki sıra numarasına bakılır. varsa sepetteki sıra no gönderilir, yoksa -1 değeri gönderilir.
                int index = isExistInCard(productId);
                // sepette eklenen ürün varsa
                if (index != -1)
                {
                    // sadece adedini gelen quantity kadar arttıracak.
                    card[index].quantity += quantity;
                }
                // sepette girilen ürün yoksa 
                else
                {
                    // sepete ekle
                    card.Add(new CardItem
                    {
                        Id = Guid.NewGuid(),
                        product = product,
                        quantity = quantity
                    }); ;
                }
                HttpContext.Session.SetString("Card", JsonSerializer.Serialize(card,options));

            }
            return Redirect(Request.GetTypedHeaders().Referer.ToString());
        }
        
    }
}
