using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;

namespace Shoper.Management.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }
        public IActionResult Index()
        {
            return View(_customerService.GetAll());
        }
    }
}
