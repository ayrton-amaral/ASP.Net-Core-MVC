using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PropertyRentalManagement.Models;

namespace PropertyRentalManagement.Controllers
{
    public class SignUpController : Controller
    {
        private readonly PropertyRentalManagementContext _context;

        public SignUpController(PropertyRentalManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,TreatmentPronoun,FirstName,LastName,Email,Username,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "SignIn");
            }
            else
            {
                return RedirectToAction("Index", "SignUp");
            }
        }
    }
}
