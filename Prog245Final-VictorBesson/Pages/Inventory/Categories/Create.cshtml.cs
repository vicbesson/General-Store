using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prog245Final_VictorBesson.Data;
using Prog245Final_VictorBesson.Models;

namespace Prog245Final_VictorBesson.Pages.Inventory.Categories
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
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}