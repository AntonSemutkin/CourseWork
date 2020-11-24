using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarbershopService.Data;
using BarbershopService.Models;
using BarbershopService.ViewModels;
using BarbershopService.ViewModels.Sorting;

namespace BarbershopService.Controllers
{
    public class ServicesController : Controller
    {
        private static IQueryable<Service> ServiceSorting(IQueryable<Service> services, Service.SortState sortOrder) =>
            sortOrder switch
            {
                Service.SortState.DateServiceAsc => services.OrderBy(s => s.DateService),
                Service.SortState.DateServiceDesc => services.OrderByDescending(s => s.DateService),
                Service.SortState.DescriptionAsc => services.OrderBy(s => s.Description),
                Service.SortState.DescriptionDesc => services.OrderByDescending(s => s.Description),
                Service.SortState.PriceAsc => services.OrderBy(s => s.Price),
                Service.SortState.PriceDesc => services.OrderByDescending(s => s.Price),
                Service.SortState.ClientAsc => services.OrderBy(s => s.Client.FullName),
                Service.SortState.ClientDesc => services.OrderByDescending(s => s.Client.FullName),
                Service.SortState.ServiceTypeAsc => services.OrderBy(s => s.ServiceType.Name),
                Service.SortState.ServiceTypeDesc => services.OrderByDescending(s => s.ServiceType.Name),
                Service.SortState.EmployeeAsc => services.OrderBy(s => s.Employee.FullName),
                Service.SortState.EmployeeDesc => services.OrderByDescending(s => s.Employee.FullName),
                _ => services
            };

        private readonly BarbershopContext _context;

        public ServicesController(BarbershopContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index(int page = 1, Service.SortState sortOrder = Service.SortState.DateServiceDesc)
        {
            int pageSize = 10;
            int itemCount = _context.Services.Count();

            IQueryable<Service> barbershopContext = _context.Services
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(s => s.Client)
                .Include(s => s.Employee)
                .Include(s => s.ServiceType);

            barbershopContext = ServiceSorting(barbershopContext, sortOrder);
            var services = await barbershopContext.ToListAsync();

            return View(new ServiceViewModel()
            {
                Services = services,
                ServicesSort = new ServicesSort(sortOrder),
                PageViewModel = new PageViewModel(itemCount, page, pageSize)
            });
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Client)
                .Include(s => s.Employee)
                .Include(s => s.ServiceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Name");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateService,Description,Price,ClientId,ServiceTypeId,EmployeeId")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName", service.ClientId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", service.EmployeeId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Name", service.ServiceTypeId);
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName", service.ClientId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", service.EmployeeId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Name", service.ServiceTypeId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateService,Description,Price,ClientId,ServiceTypeId,EmployeeId")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName", service.ClientId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", service.EmployeeId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Name", service.ServiceTypeId);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Client)
                .Include(s => s.Employee)
                .Include(s => s.ServiceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
