using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyRentalManagement.Models;

namespace PropertyRentalManagement.Controllers
{
    public class TenantsController : Controller
    {
        private readonly PropertyRentalManagementContext _context;

        public TenantsController(PropertyRentalManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            String currentUsername = HttpContext.User.FindFirst("username").Value;
            var currentUser = _context.Users.FirstOrDefault(u => u.Username == currentUsername);
            // Buscar no banco a lista de Apts Available
            var messages = _context.Messages.Include(m => m.Sender).Where(m => m.ReceiverId == currentUser.UserId);

            return View(await messages.ToListAsync());
        }

        // View Apartments Available Only

        // Schedule an Appointment with the Manager

        // Send Message to Manager
        public IActionResult SendMessage()
        {
            // Create a ViewModel to pass necessary data to the view, like manager IDs or names.
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                // Identifying my User:
                String currentUsername = HttpContext.User.FindFirst("username").Value;
                User currentUser = _context.Users.FirstOrDefault(c => c.Username == currentUsername);
                message.SenderId = currentUser.UserId;

                //// Identifying my Manager:
                //String managerRole = currentUser.Role ; // Assuming ManagerID is a field in the Users table
                //message.ReceiverId = managerRole;

                // Save the message to the database
                _context.Messages.Add(message);
                _context.SaveChanges();

                return RedirectToAction("Index", "Tenants");
            }

            return View(message);
        }
    }
}
