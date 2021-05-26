using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prog245Final_VictorBesson.Data;
using Prog245Final_VictorBesson.Models;

namespace Prog245Final_VictorBesson.Pages.Inventory.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Prog245Final_VictorBesson.Data.ApplicationDbContext _context;

        public IndexModel(Prog245Final_VictorBesson.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
