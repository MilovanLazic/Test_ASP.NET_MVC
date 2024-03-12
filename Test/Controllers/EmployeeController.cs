using Microsoft.AspNetCore.Mvc;
using Test.BusinessLogic.Interfaces;

namespace Test.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeLogic _employeeLogic;

        public EmployeeController(IEmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }
        public async Task<IActionResult> Index()
        {
            var employess = await _employeeLogic.GetAll();
            return View(employess);
        }
    }
}
