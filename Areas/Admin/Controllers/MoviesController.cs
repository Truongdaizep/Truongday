using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NETCORE.Models;

namespace NETCORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hoaqua.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaqua = await _context.Hoaqua
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoaqua == null)
            {
                return NotFound();
            }

            return View(hoaqua);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Price,ImageUrl")] Hoaqua hoaqua)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaqua);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hoaqua);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaqua = await _context.Hoaqua.FindAsync(id);
            if (hoaqua == null)
            {
                return NotFound();
            }
            return View(hoaqua);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Price,ImageUrl")] Hoaqua hoaqua)
        {
            if (id != hoaqua.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaqua);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaquaExists(hoaqua.Id))
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
            return View(hoaqua);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaqua = await _context.Hoaqua
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoaqua == null)
            {
                return NotFound();
            }

            return View(hoaqua);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaqua = await _context.Hoaqua.FindAsync(id);
            if (hoaqua != null)
            {
                _context.Hoaqua.Remove(hoaqua);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaquaExists(int id)
        {
            return _context.Hoaqua.Any(e => e.Id == id);
        }
    }
}
