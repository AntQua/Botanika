using CLOUD462022.Context;
using CLOUD462022.Models;
using CLOUD462022.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController : Controller
    {
        private readonly CLOUD462022DbContext _context;

        public AdminOrdersController(CLOUD462022DbContext context)
        {
            _context = context;
        }


        public IActionResult OrderProducts(int? id)
        {
            var order = _context.Orders
                          .Include(o => o.OrderItems)
                          .ThenInclude(p => p.Product)
                          .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                Response.StatusCode = 404;
                return View("OrderNotFound", id.Value);
            }

            OrderProductViewModel orderProducts = new OrderProductViewModel()
            {
                Order = order,
                OrderItems = order.OrderItems
            };
            return View(orderProducts);
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "FirstName")
        {
            var result = _context.Orders.AsNoTracking()
                           .AsQueryable();


            if (!string.IsNullOrWhiteSpace(filter))
            {
                result = result.Where(o => o.FirstName.Contains(filter));
            }

            var model = await PagingList.CreateAsync(result, 5, pageindex, sort, "FirstName");

            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }


        // GET: Admin/AdminOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        // GET: Admin/AdminOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Admin/AdminOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,FirstName,LastName,Address,CityCode,City,PhoneNumber,Email,OrderDate,OrderDeliveryDate")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            return View(order);
        }

        // GET: Admin/AdminOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
