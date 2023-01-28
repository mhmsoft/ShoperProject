using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;
using Shoper.Entities;

namespace Shoper.Management.Controllers
{
   
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;
        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService,IProductService productService )
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAll();
            return View(orders);
        }
        public IActionResult OrderDetail(int id)
        {
            var order = _orderDetailService.GetExp(x=>x.OrderId==id);
            return View(order);
        }
        [HttpPost]
        public bool Confirm(int id)
        {
            var order=_orderService.Get(id);            
            order.isVerified = true;
            order.isActive = true;
            var result=_orderService.Update(order);
            return result!=null?true:false;
        }
        [HttpPost]
        public bool Delete(int id)  //iade
        {
            var order = _orderService.Get(id);
            order.isActive = false;
            order.isVerified = false;
            order.Status = OrderStatus.Cancelled;
            var products = _orderDetailService.GetExp(x => x.OrderId == order.OrderId);
            foreach (var product in products)
            {
                var canceledProduct = _productService.Get(product.Product.ProductId);
                    canceledProduct.ProductStock += product.quantity;
                _productService.Update(canceledProduct);
            }
            var result=_orderService.Update(order);
            return result != null ? true : false;
        }
    }
}
