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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Shoper.BusinessLogic.Utility;
using MimeKit;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shoper.UI.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IProductCommentService _productCommentService;
        private readonly IOrderService _orderService;
        private readonly IAddressService _addressService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailSender _mailSender;
        public CatalogController(IProductService productService,
                                ICustomerService customerService,
                                IOrderService   orderService,
                                IAddressService addressService,
                                IOrderDetailService orderDetailService,
                                IProductCommentService productCommentService,
                                UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                IMailSender mailSender)
        {
            _productService= productService;
            _customerService= customerService;
            _orderService= orderService;
            _orderDetailService=orderDetailService;
            _addressService=addressService;
            _productCommentService= productCommentService;
            _userManager= userManager;
            _signInManager= signInManager;
            _mailSender= mailSender;
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

        #region Card
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
        #endregion

        #region Checkout
       
        public async Task<IActionResult> Checkout()
        {
            if(_signInManager.IsSignedIn(User))
                {
                string email = User.Identity.Name;
                var user = await _userManager.FindByEmailAsync(email);
                Customer customer = _customerService.GetExp(x => x.UserId == user.Id).FirstOrDefault();
                return View(customer);
            }
               return View();

        }
        [HttpPost]
        public async Task<IActionResult> Checkout(string amount, string fullName,string phone,string email,string city,string adres1,string adres2,string selectedAddress)
        {
            try
            {
                List<CardItem> card = new List<CardItem>();
                string Email = "";
                var cardSession = HttpContext.Session.GetString("Card");
                card = JsonSerializer.Deserialize<List<CardItem>>(cardSession);

                if (_signInManager.IsSignedIn(User)) // login olan müşteri
                {
                    Email = User.Identity.Name;
                    var user = await _userManager.FindByEmailAsync(Email);
                    Customer customer = _customerService.GetExp(x => x.UserId == user.Id).FirstOrDefault();


                    if (!string.IsNullOrEmpty(selectedAddress)) // mevcut bir adres seçmişse
                    {
                        Email = customer.Email;
                        Order order = new Order()
                        {
                            OrderDate = DateTime.Now,
                            Adres1 = customer.Addresses.FirstOrDefault(x => x.AddressId == Int32.Parse(selectedAddress)).Address1,
                            Adres2 = customer.Addresses.FirstOrDefault(x => x.AddressId == Int32.Parse(selectedAddress)).Address2,
                            City = customer.Addresses.FirstOrDefault(x => x.AddressId == Int32.Parse(selectedAddress)).City,
                            fullName = customer.FirstName + " " + customer.LastName,
                            mail = Email,
                            Phone = customer.Phone,
                            Status = OrderStatus.Preparing,
                            TotalAmount = float.Parse(amount),
                            CustomerId = customer.CustomerId
                        };
                        order.isActive = true;
                        order.isVerified = false;
                        var result = _orderService.Add(order); // sipariş kaydedildi
                        foreach (var item in card)
                        {
                            // stock kontrolü
                            if (_productService.Get(item.product.ProductId).ProductStock < item.quantity)
                            {
                                _orderService.Delete(result);
                                throw new Exception("Yeteri miktarda ürün olmadığından siparişiniz alınamamıştır.");
                                
                            }
                            else
                            {

                                OrderDetail orderDetail = new OrderDetail()
                                {
                                    OrderId = result.OrderId,
                                    ProductId = item.product.ProductId,
                                    quantity = item.quantity
                                };
                                var OrderedProduct = _productService.Get(item.product.ProductId);
                                OrderedProduct.ProductStock -= item.quantity;
                                _productService.Update(OrderedProduct);
                                _orderDetailService.Add(orderDetail);
                            }
                        }
                    }
                    else // yeni bir adres girmişse
                    {
                        Email = customer.Email;
                        Order order = new Order()
                        {
                            OrderDate = DateTime.Now,
                            Adres1 = adres1,
                            Adres2 = adres2,
                            City = city,
                            fullName = customer.FirstName + " " + customer.LastName,
                            mail = Email,
                            Phone = customer.Phone,
                            Status = OrderStatus.Preparing,
                            TotalAmount = float.Parse(amount),
                            CustomerId = customer.CustomerId
                        };
                        Address address = new Address()
                        {
                            Address1 = adres1,
                            Address2 = adres2,
                            AddressTitle = "",
                            CustomerId = customer.CustomerId,
                            City = city
                        };
                        _addressService.Add(address);
                        order.isActive = true;
                        order.isVerified = false;
                        var result = _orderService.Add(order); // sipariş kaydedildi

                        foreach (var item in card)
                        {
                            // stock kontrolü
                            if (_productService.Get(item.product.ProductId).ProductStock < item.quantity)
                            {
                                _orderService.Delete(result);
                                throw new Exception("Yeteri miktarda ürün olmadığından siparişiniz alınamamıştır.");

                            }
                            else
                            {
                                OrderDetail orderDetail = new OrderDetail()
                                {
                                    OrderId = result.OrderId,
                                    ProductId = item.product.ProductId,
                                    quantity = item.quantity

                                };
                                var OrderedProduct = _productService.Get(item.product.ProductId);
                                OrderedProduct.ProductStock -= item.quantity;
                                _productService.Update(OrderedProduct);
                                _orderDetailService.Add(orderDetail);
                            }
                           
                        }
                    }
                }
                else // login olmayan müşteri
                {
                    Email = email;
                    Customer customer = new Customer()
                    {
                        Email = Email,
                        FirstName = fullName,
                        LastName = "",
                        Phone = phone
                    };
                    if (_customerService.GetExp(x => x.Email == Email).FirstOrDefault() == null)
                    {
                        var customerResult = _customerService.Add(customer); //login olmayan müşteri ekleniyor
                        Address address = new Address()
                        {
                            Address1 = adres1,
                            Address2 = adres2,
                            AddressTitle = "Ev",
                            City = city,
                            CustomerId = customerResult.CustomerId
                        };
                        _addressService.Add(address); // login olmayan müşterin adresi ekleniyor
                        Order order = new Order()
                        {
                            OrderDate = DateTime.Now,
                            Adres1 = adres1,
                            Adres2 = adres2,
                            City = city,
                            fullName = fullName,
                            mail = Email,
                            Phone = phone,
                            Status = OrderStatus.Preparing,
                            TotalAmount = float.Parse(amount),
                            CustomerId = customerResult.CustomerId
                        };
                        order.isActive = true;
                        order.isVerified = false;
                        var result = _orderService.Add(order); // sipariş kaydedildi
                        foreach (var item in card)
                        {
                            // stock kontrolü
                            if (_productService.Get(item.product.ProductId).ProductStock < item.quantity)
                            {
                                _orderService.Delete(result);
                                throw new Exception("Yeteri miktarda ürün olmadığından siparişiniz alınamamıştır.");

                            }
                            else
                            {
                                OrderDetail orderDetail = new OrderDetail()
                                {
                                    OrderId = result.OrderId,
                                    ProductId = item.product.ProductId,
                                    quantity = item.quantity

                                };
                                var OrderedProduct = _productService.Get(item.product.ProductId);
                                OrderedProduct.ProductStock -= item.quantity;
                                _productService.Update(OrderedProduct);
                                _orderDetailService.Add(orderDetail);
                            }

                        }
                    }
                }
                var emailResult = await _mailSender.MailSend(new Email()
                {
                    Subject = "Sipraşiniz olşturuldu",
                    Body = $"Siparişinizi aldık.",
                    To = InternetAddress.Parse(Email)
                });
                HttpContext.Session.Remove("Card"); // sepet Session siliniyor

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                throw;
            }
           
            return RedirectToAction("MyOrders","Account");
        }
        #endregion

    }
}
