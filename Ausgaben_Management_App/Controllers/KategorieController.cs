using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ausgaben_Management_App.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Ausgaben_Management_App.Controllers
{
    public class KategorieController : Controller
    {
        private readonly AppDbContext _context;

        public KategorieController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Kategorie
        public async Task<IActionResult> Index()
        {
              return _context.TbKategorien != null ? 
                          View(await _context.TbKategorien.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.TbKategorien'  is null.");
        }

        // GET: Kategorie/Details/5
       
        // GET: Kategorie/AddOrEdit
        public IActionResult AddOrEdit(int id=0)
        {
            if (id==0)
            {
                return View(new TbKategorie());
            }
            else
            return View(_context.TbKategorien.Find(id));
        }

        // POST: TbKategorie/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("kategorieId,Titel,Icon,Type")] TbKategorie tbKategorie)
        {
            if (ModelState.IsValid)
            {
                if(tbKategorie.kategorieId == 0) 
                    
                _context.Add(tbKategorie);
                else
                    _context.Update(tbKategorie);
                    
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbKategorie);
        }

        // GET: Kategorie/Edit/5
       

        // POST: Kategorie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
       

        // GET: Kategorie/Delete/5
       

        // POST: Kategorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbKategorien == null)
            {
                return Problem("Entity set 'AppDbContext.TbKategorien'  is null.");
            }
            var tbKategorie = await _context.TbKategorien.FindAsync(id);
            if (tbKategorie != null)
            {
                _context.TbKategorien.Remove(tbKategorie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
