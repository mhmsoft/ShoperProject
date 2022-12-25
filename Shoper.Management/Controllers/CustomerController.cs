using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoper.BusinessLogic.Interface;
using System.Data;

namespace Shoper.Management.Controllers
{
    [Authorize(Roles = "manager")]
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
