using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ausgaben_Management_App.Models;

namespace Ausgaben_Management_App.Controllers
{
    public class TransaktionController : Controller
    {
        private readonly AppDbContext _context;

        public TransaktionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Transaktion
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TbTransaktionen.Include(t => t.kategorie);
            return View(await appDbContext.ToListAsync());
        }



        // GET: Transaktion/AddOrEdit
        public IActionResult AddOrEdit(int id =0)
        {
            Populatecategories();
            if(id==0)

            return View( new TbTransaktion());
            else
                return View(_context.TbTransaktionen.Find(id));
        }

        // POST: Transaktion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransaktionId,kategorieId,Betrag,Note,Datum")] TbTransaktion tbTransaktion)
        {
            if (ModelState.IsValid)
            {
                if(tbTransaktion.TransaktionId==0)

                _context.Add(tbTransaktion);
                else
                    _context.Update(tbTransaktion);    
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Populatecategories();
            return View(tbTransaktion);
        }

        
       
        // POST: Transaktion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbTransaktionen == null)
            {
                return Problem("Entity set 'AppDbContext.TbTransaktionen'  is null.");
            }
            var tbTransaktion = await _context.TbTransaktionen.FindAsync(id);
            if (tbTransaktion != null)
            {
                _context.TbTransaktionen.Remove(tbTransaktion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void Populatecategories()
        {
            var categoryCollection = _context.TbKategorien.ToList();
            TbKategorie Defaultcategory = new TbKategorie() { kategorieId = 0, Titel = "Kategorie auswällen" };
            categoryCollection.Insert(0, Defaultcategory);
            ViewBag.TbKategorien = categoryCollection;

        }
    }
}
