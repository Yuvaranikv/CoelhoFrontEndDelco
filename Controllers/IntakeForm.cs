using Microsoft.AspNetCore.Mvc;

namespace TURN.Controllers
{
    public class IntakeForm :Controller
    {
        // GET: /IntakeForm
        [HttpGet]
        public IActionResult Index()
        {
            // Return the view for IntakeForm
            return View();
        }
    }
}
