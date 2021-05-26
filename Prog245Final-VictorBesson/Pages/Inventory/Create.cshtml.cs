using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prog245Final_VictorBesson.Data;
using Prog245Final_VictorBesson.Models;

namespace Prog245Final_VictorBesson.Pages.Inventory
{
    public class CreateModel : PageModel
    {
        private readonly Prog245Final_VictorBesson.Data.ApplicationDbContext _context;

        public CreateModel(Prog245Final_VictorBesson.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Product.Created = DateTime.Now;
            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}