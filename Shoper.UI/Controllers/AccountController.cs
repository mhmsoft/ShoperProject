using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;
using Shoper.BusinessLogic.Service;
using Shoper.Entities;

namespace Shoper.UI.Controllers
{
    [Authorize(Roles ="customer")]
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;
        private readonly ICouponService _couponService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;
        private readonly IWishListService _wishListService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(IOrderService orderService,
                                 ICustomerService customerService,
                                 UserManager<AppUser> userManager,
                                 IOrderDetailService orderDetailService,
                                 ICouponService couponService,
                                 IProductService productService,
                                 SignInManager<AppUser> signInManager,
                                 IWishListService wishListService,
                                 IAddressService addressService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _userManager = userManager;
            _signInManager = signInManager;
            _orderDetailService = orderDetailService;
            _productService = productService;
            _wishListService = wishListService;
            _couponService = couponService;
            _addressService = addressService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyOrders()
        {
            var username=User.Identity.Name; // as mail
            List<Order> orders= _customerService.GetExp(x => x.Email == username).FirstOrDefault().Orders.ToList();
            return View(orders);
        }
        #region WishList
        public IActionResult MyWishList()
        {
            int customerId = _customerService.GetExp(x => x.Email == User.Identity.Name).FirstOrDefault().CustomerId;
            var myWishList = _wishListService.GetExp(x => x.customerId == customerId);
            return View(myWishList);
        }

        [HttpPost]
        public bool AddWishList(int productId)
        {
            var queryWish = _wishListService.GetExp(x => x.productId == productId);
            if (queryWish != null)
            {
                WishList wishList = new WishList()
                {
                    added = DateTime.Now,
                    productId = productId,
                    customerId = _customerService.GetExp(x => x.Email == User.Identity.Name).FirstOrDefault().CustomerId
                };
                _wishListService.Add(wishList);
                return true;
            }
            return false;
        }
        #endregion
        public IActionResult MyCoupons()
        {
            var customerId = _customerService.GetExp(x => x.Email == User.Identity.Name).FirstOrDefault().CustomerId;
            var myCoupons = _couponService.GetExp(x => x.customerId == customerId);
            return View(myCoupons);
        }
        public IActionResult MyAddresses()
        {
            var addresses= _customerService.GetExp(x => x.Email == User.Identity.Name).FirstOrDefault().Addresses.ToList();
            return View(addresses);
        }
        public IActionResult NewAddress()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewAddress(Address model)
        {
            var customerId = _customerService.GetExp(x => x.Email == User.Identity.Name).FirstOrDefault().CustomerId;
            model.CustomerId = customerId;
            _addressService.Add(model);
            return RedirectToAction("MyAddresses");
        }

        public IActionResult editAddress(int AddressId)
        {
            var address = _addressService.Get(AddressId);
            return View(address);
        }
        [HttpPost]
        public IActionResult editAddress(Address model)
        {

            var customerId = _customerService.GetExp(x => x.Email == User.Identity.Name).FirstOrDefault().CustomerId;
            model.CustomerId = customerId;
            _addressService.Update(model);
            return RedirectToAction("MyAddresses");
        }
        [HttpPost]
        public bool deleteAddress(int id)
        {
            var address = _addressService.Get(id);
            return _addressService.Delete(address)!=null;
        }

        public IActionResult MyProfile()
        {
            var customer = _customerService.GetExp(x => x.Email == User.Identity.Name).FirstOrDefault();
            return View(customer);
        }
        [HttpPost]
        public IActionResult MyProfile(Customer model)
        {
            var result=_customerService.Update(model);
            return View(result);
        }
        [HttpPost]
        public bool CancelOrder(int id)  //iade
        {
            var order = _orderService.Get(id);
            order.isActive = false;
            order.isVerified = false;
            order.Status=OrderStatus.Cancelled;
            var products = _orderDetailService.GetExp(x => x.OrderId == order.OrderId);
            foreach (var product in products)
            {
                var canceledProduct = _productService.Get(product.Product.ProductId);
                canceledProduct.ProductStock += product.quantity;
                _productService.Update(canceledProduct);
            }
            var result = _orderService.Update(order);
            return result != null ? true : false;
        }
        public IActionResult OrderDetail(int id)
        {
            var order = _orderDetailService.GetExp(x => x.OrderId == id);
            return View(order);
        }

    }
}
