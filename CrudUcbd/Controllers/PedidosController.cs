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
    public class PedidosController : Controller
    {
        private readonly Contexto _context;

        public PedidosController(Contexto context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            return _context.Pedidos != null ?
                        View(await _context.Pedidos.ToListAsync()) :
                        Problem("Entity set 'Contexto.Pedidos'  is null.");
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedido,NomeProduto,Valor,DataVencimento")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedido,NomeProduto,Valor,DataVencimento")] Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.IdPedido))
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
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'Contexto.Pedidos'  is null.");
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return (_context.Pedidos?.Any(e => e.IdPedido == id)).GetValueOrDefault();
        }

        public string vencimentoProduto(DateTime dataVencimento)
        {

            DateTime dataAtual = DateTime.Now;

            string stringDataAtual = dataAtual.ToString();
            string stringDataVencimento = dataVencimento.ToString();
            string estadoVencimento = "";
            int valorFinal;

            char[] remover = new char[] { '/', ' ' };

            string[] arrayDataAtual = stringDataAtual.Split(remover, StringSplitOptions.RemoveEmptyEntries);

            string[] parteDataAtual = new string[arrayDataAtual.Length];

            for (int i = 0; i < arrayDataAtual.Length; i++)
            {
                parteDataAtual[i] = arrayDataAtual[i];
            }

            string[] arrayDataVencimento = stringDataVencimento.Split(remover, StringSplitOptions.RemoveEmptyEntries);

            string[] parteDataVencimento = new string[arrayDataVencimento.Length];

            for (int i = 0; i < arrayDataVencimento.Length; i++)
            {
                parteDataVencimento[i] = arrayDataVencimento[i];
            }

            valorFinal = (Int32.Parse(parteDataAtual[0])
                + Int32.Parse(parteDataAtual[1]) * 30
                + Int32.Parse(parteDataAtual[2]) * 365)
                - (Int32.Parse(parteDataVencimento[0])
                + Int32.Parse(parteDataVencimento[1]) * 30
                + Int32.Parse(parteDataVencimento[2]) * 365);

            if (dataVencimento <= dataAtual) estadoVencimento = "Válido";
            else if (dataVencimento > dataAtual) estadoVencimento = "Vencido";

            if ((dataVencimento <= dataAtual) && (valorFinal <= 3)) estadoVencimento = "Estão quase vencendo";
            return estadoVencimento;
        }
    }
}    