using Microsoft.AspNetCore.Mvc;

namespace TURN.Controllers
{
    public class IntakeFamilyMember:Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
