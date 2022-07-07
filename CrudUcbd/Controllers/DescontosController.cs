using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudUcbd.Models;

namespace CrudUcbd.Controllers
{
    public class DescontosController : Controller
    {
        private readonly Contexto _context;

        public DescontosController(Contexto context)
        {
            _context = context;
        }

        // GET: Descontos
        public async Task<IActionResult> Index()
        {
              return _context.Descontos != null ? 
                          View(await _context.Descontos.ToListAsync()) :
                          Problem("Entity set 'Contexto.Descontos'  is null.");
        }

        // GET: Descontos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Descontos == null)
            {
                return NotFound();
            }

            var desconto = await _context.Descontos
                .FirstOrDefaultAsync(m => m.DescontoId == id);
            if (desconto == null)
            {
                return NotFound();
            }

            return View(desconto);
        }

        // GET: Descontos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Descontos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DescontoId,PedidoId,ValorDesconto")] Desconto desconto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(desconto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(desconto);
        }

        // GET: Descontos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Descontos == null)
            {
                return NotFound();
            }

            var desconto = await _context.Descontos.FindAsync(id);
            if (desconto == null)
            {
                return NotFound();
            }
            return View(desconto);
        }

        // POST: Descontos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DescontoId,PedidoId,ValorDesconto")] Desconto desconto)
        {
            if (id != desconto.DescontoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desconto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescontoExists(desconto.DescontoId))
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
            return View(desconto);
        }

        // GET: Descontos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Descontos == null)
            {
                return NotFound();
            }

            var desconto = await _context.Descontos
                .FirstOrDefaultAsync(m => m.DescontoId == id);
            if (desconto == null)
            {
                return NotFound();
            }

            return View(desconto);
        }

        // POST: Descontos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Descontos == null)
            {
                return Problem("Entity set 'Contexto.Descontos'  is null.");
            }
            var desconto = await _context.Descontos.FindAsync(id);
            if (desconto != null)
            {
                _context.Descontos.Remove(desconto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescontoExists(int id)
        {
          return (_context.Descontos?.Any(e => e.DescontoId == id)).GetValueOrDefault();
        }
        public string Descontar(string estadoVencimento, double desconto)
        {
            if (estadoVencimento == "Válido" || estadoVencimento == "Estão quase vencendo")
            {
                double valorProduto = 5;
                double valorFinalDescontado = valorProduto - (valorProduto * desconto);

                string resultado = valorFinalDescontado.ToString();

                return resultado;
            }
            return "Produto vencido";
        }
    }

}
