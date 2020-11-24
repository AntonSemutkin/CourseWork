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
using BarbershopService.ViewModels.Filters;
using BarbershopService.ViewModels.Sorting;

namespace BarbershopService.Controllers
{
    public class ClientsController : Controller
    {
        private static IQueryable<Client> ClientSorting(IQueryable<Client> clients, Client.SortState sortOrder) =>
            sortOrder switch
            {
                Client.SortState.FullNameAsc => clients.OrderBy(c => c.FullName),
                Client.SortState.FullNameDesc => clients.OrderByDescending(c => c.FullName),
                Client.SortState.AddressAsc => clients.OrderBy(c => c.Address),
                Client.SortState.AddressDesc => clients.OrderByDescending(c => c.Address),
                Client.SortState.PhoneNumberAsc => clients.OrderBy(c => c.PhoneNumber),
                Client.SortState.PhoneNumberDesc => clients.OrderByDescending(c => c.PhoneNumber),
                Client.SortState.DiscountAsc => clients.OrderBy(c => c.Discount),
                Client.SortState.DiscountDesc => clients.OrderByDescending(c => c.Discount),
                _ => clients
            };
        
        private readonly BarbershopContext _context;

        public ClientsController(BarbershopContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(int? selectedServiceTypeId, int? reviewMark, double? discount,
            int page = 1, Client.SortState sortOrder = Client.SortState.FullNameDesc)
        {
            int pageSize = 10;
            int itemCount = _context.Clients.Count();

            IQueryable<Client> clientsContext = _context.Clients;

            if (selectedServiceTypeId.HasValue && selectedServiceTypeId != 0)
            {
                clientsContext = clientsContext
                    .SelectMany(c => c.Services.Where(s => s.ServiceTypeId == selectedServiceTypeId))
                    .Select(s => s.Client);
            }

            if (reviewMark.HasValue)
            {
                clientsContext = clientsContext
                    .SelectMany(c => c.Reviews.Where(r => r.ClientMark == reviewMark))
                    .Select(r => r.Client);
            }

            if (discount.HasValue)
            {
                clientsContext = clientsContext
                    .Where(c => c.Discount >= discount);
            }

            clientsContext = ClientSorting(clientsContext, sortOrder);

            var clients = await clientsContext
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var serviceTypes = await _context.ServiceTypes.ToListAsync();
            return View(new ClientViewModel()
            {
                ClientFilter = new ClientFilter(selectedServiceTypeId, serviceTypes, reviewMark,  discount),
                ClientSort = new ClientSort(sortOrder),
                PageViewModel = new PageViewModel(itemCount, page, pageSize),
                Clients = clients
            });
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Address,PhoneNumber,Discount")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Address,PhoneNumber,Discount")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
