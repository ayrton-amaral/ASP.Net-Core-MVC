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
    public class ApartmentsController : Controller
    {
        private readonly PropertyRentalManagementContext _context;

        public ApartmentsController(PropertyRentalManagementContext context)
        {
            _context = context;
        }

        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            var propertyRentalManagementContext = _context.Apartments.Include(a => a.Availability).Include(a => a.Building).Include(a => a.Manager).Include(a => a.Owner).Include(a => a.Tenant);
            return View(await propertyRentalManagementContext.ToListAsync());
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.Availability)
                .Include(a => a.Building)
                .Include(a => a.Manager)
                .Include(a => a.Owner)
                .Include(a => a.Tenant)
                .FirstOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            ViewData["AvailabilityId"] = new SelectList(_context.Availabilities, "AvailabilityId", "AvailabilityDescription");

            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingName");

            var owners = _context.Users.Where(m => m.Role == "Owner");
            ViewData["OwnerId"] = new SelectList(owners, "UserId", "Username");

            var managers = _context.Users.Where(m => m.Role == "Manager");
            ViewData["ManagerId"] = new SelectList(managers, "UserId", "Username");

            var tenants = new List<User>();
            tenants.Add(new User());
            tenants.AddRange(_context.Users.Where(u => u.Role == "Tenant").ToList());
            ViewData["TenantId"] = new SelectList(tenants, "UserId", "Username");

            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,ApartmentNumber,ApartmentFloor,ApartmentType,AvailabilityId,RentPrice,BuildingId,OwnerId,ManagerId,TenantId")] Apartment apartment)
        {
            if(apartment.TenantId == 0)
            {
                apartment.TenantId = null;
            }
            //if (ModelState.IsValid)
            //{
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["AvailabilityId"] = new SelectList(_context.Availabilities, "AvailabilityId", "AvailabilityId", apartment.AvailabilityId);
            //ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartment.BuildingId);
            //ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "UserId", apartment.ManagerId);
            //ViewData["OwnerId"] = new SelectList(_context.Users, "UserId", "UserId", apartment.OwnerId);
            //ViewData["TenantId"] = new SelectList(_context.Users, "UserId", "UserId", apartment.TenantId);
            //return View(apartment);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }

            ViewData["AvailabilityId"] = new SelectList(_context.Availabilities, "AvailabilityId", "AvailabilityDescription", apartment.AvailabilityId);

            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingName", apartment.BuildingId);

            var owners = _context.Users.Where(m => m.Role == "Owner");
            ViewData["OwnerId"] = new SelectList(owners, "UserId", "Username", apartment.OwnerId);

            var managers = _context.Users.Where(m => m.Role == "Manager");
            ViewData["ManagerId"] = new SelectList(managers, "UserId", "Username", apartment.ManagerId);

            var tenants = new List<User>();
            tenants.Add(new User());
            tenants.AddRange(_context.Users.Where(u => u.Role == "Tenant").ToList());
            ViewData["TenantId"] = new SelectList(tenants, "UserId", "Username", apartment.TenantId);

            return View(apartment);
        }

        // POST: Apartments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApartmentId,ApartmentNumber,ApartmentFloor,ApartmentType,AvailabilityId,RentPrice,BuildingId,OwnerId,ManagerId,TenantId")] Apartment apartment)
        {
            if (id != apartment.ApartmentId)
            {
                return NotFound();
            }

            if (apartment.TenantId == 0)
            {
                apartment.TenantId = null;
            }

            //if (ModelState.IsValid)
            //{
            try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.ApartmentId))
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
            //ViewData["AvailabilityId"] = new SelectList(_context.Availabilities, "AvailabilityId", "AvailabilityId", apartment.AvailabilityId);
            //ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartment.BuildingId);
            //ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "UserId", apartment.ManagerId);
            //ViewData["OwnerId"] = new SelectList(_context.Users, "UserId", "UserId", apartment.OwnerId);
            //ViewData["TenantId"] = new SelectList(_context.Users, "UserId", "UserId", apartment.TenantId);
            //return View(apartment);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.Availability)
                .Include(a => a.Building)
                .Include(a => a.Manager)
                .Include(a => a.Owner)
                .Include(a => a.Tenant)
                .FirstOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment != null)
            {
                _context.Apartments.Remove(apartment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartments.Any(e => e.ApartmentId == id);
        }
    }
}
