using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ListaTelefonica.Data;
using ListaTelefonica.Models;

namespace ListaTelefonica.Controllers
{
    public class ListaController : Controller
    {
        private readonly ListaTelefonicaContext _context;

        public ListaController(ListaTelefonicaContext context)
        {
            _context = context;
        }

        // GET: Lista
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lista.ToListAsync());
        }

        // GET: Lista/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _context.Lista
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lista == null)
            {
                return NotFound();
            }

            return View(lista);
        }

        // GET: Lista/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Telefone")] Lista lista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lista);
        }

        // GET: Lista/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _context.Lista.FindAsync(id);
            if (lista == null)
            {
                return NotFound();
            }
            return View(lista);
        }

        // POST: Lista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Telefone")] Lista lista)
        {
            if (id != lista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaExists(lista.Id))
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
            return View(lista);
        }

        // GET: Lista/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _context.Lista
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lista == null)
            {
                return NotFound();
            }

            return View(lista);
        }

        // POST: Lista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lista = await _context.Lista.FindAsync(id);
            if (lista != null)
            {
                _context.Lista.Remove(lista);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaExists(int id)
        {
            return _context.Lista.Any(e => e.Id == id);
        }
    }
}
