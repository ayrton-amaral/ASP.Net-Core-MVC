using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyRentalManagement.Models;

namespace PropertyRentalManagement.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly PropertyRentalManagementContext _context;

        public AppointmentsController(PropertyRentalManagementContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.Username.Equals(HttpContext.User.FindFirst("username").Value));

            if(currentUser.Role == "Tenant")
            {
                var propertyRentalManagementContext = _context.Appointments.Include(a => a.Manager).Include(a => a.Tenant).Where(appointment => appointment.TenantId == currentUser.UserId);
                return View(await propertyRentalManagementContext.ToListAsync());
            }
            
            return View(await _context.Appointments.Include(a => a.Manager).Include(a => a.Tenant).ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Manager)
                .Include(a => a.Tenant)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            String currentUsername = HttpContext.User.FindFirst("username").Value;

            var currentUser = _context.Users.FirstOrDefault(u => u.Username == currentUsername);

            List<User> loggedUser = new List<User>();
            loggedUser.Add(currentUser);

            if (currentUser.Role != "Tenant") // Manager and Owner
            {
                ViewData["ManagerId"] = new SelectList(loggedUser, "UserId", "Username");

                var tenants = _context.Users.Where(t => t.Role == "Tenant");

                ViewData["TenantId"] = new SelectList(tenants, "UserId", "Username");
            }
            else // Tenant
            {
                ViewData["TenantId"] = new SelectList(loggedUser, "UserId", "Username");

                var managers = _context.Users.Where(m => m.Role == "Manager");

                ViewData["ManagerId"] = new SelectList(managers, "UserId", "Username");
            }

            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,AppointmentDate,AppointmentTime,TenantId,ManagerId")] Appointment appointment)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "Username", appointment.ManagerId);
            //ViewData["TenantId"] = new SelectList(_context.Users, "UserId", "Username", appointment.TenantId);
            //return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            String currentUsername = HttpContext.User.FindFirst("username").Value;

            var currentUser = _context.Users.FirstOrDefault(u => u.Username == currentUsername);

            List<User> loggedUser = new List<User>();
            loggedUser.Add(currentUser);

            if (currentUser.Role != "Tenant") // Manager and Owner
            {
                ViewData["ManagerId"] = new SelectList(loggedUser, "UserId", "Username");

                var tenants = _context.Users.Where(t => t.Role == "Tenant");

                ViewData["TenantId"] = new SelectList(tenants, "UserId", "Username");
            }
            else // Tenant
            {
                ViewData["TenantId"] = new SelectList(loggedUser, "UserId", "Username");

                var managers = _context.Users.Where(m => m.Role == "Manager");

                ViewData["ManagerId"] = new SelectList(managers, "UserId", "Username");
            }

            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,AppointmentDate,AppointmentTime,TenantId,ManagerId")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //ViewDaa["ManagerId"] = new SelectList(_context.Users, "UserId", "Username", appointment.ManagerId);
            //ViewData["TenantId"] = new SelectList(_context.Users, "UserId", "Username", appointment.TenantId);
            //return View(tappointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Manager)
                .Include(a => a.Tenant)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
