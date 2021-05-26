using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prog245Final_VictorBesson.Models;

namespace Prog245Final_VictorBesson.Pages.Inventory
{
    public class PublicInventoryModel : PageModel
    {
        public readonly Prog245Final_VictorBesson.Data.ApplicationDbContext _context;

        public PublicInventoryModel(Prog245Final_VictorBesson.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; }
        public IList<Category> Category { get; set; }
        public IList<Item> Item { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
            Category = await _context.Category.Include(x => x.Products).ToListAsync();
            ShoppingCart = await _context.ShoppingCart.Where(x => x.aspNetUser.UserName == User.Identity.Name).FirstOrDefaultAsync();
            if(ShoppingCart != null)
                Item = await _context.Item.Where(x => x.ShoppingCart == ShoppingCart).ToListAsync();
        }
        public async Task<IActionResult> OnPostAdd(string val)
        {
            Product = await _context.Product.ToListAsync();
            Category = await _context.Category.Include(x => x.Products).ToListAsync();
            ShoppingCart = await _context.ShoppingCart.Where(x => x.aspNetUser.UserName == User.Identity.Name).FirstOrDefaultAsync();
            if (ShoppingCart != null)
                Item = await _context.Item.Where(x => x.ShoppingCart == ShoppingCart).ToListAsync();
            int id = int.Parse(val);
            Item tmpitem = await _context.Item.Where(x => x.ProductID == id && x.ShoppingCart == ShoppingCart).FirstOrDefaultAsync();
            if(tmpitem != null && tmpitem.AmountInCart < _context.Product.Where( x => x.ProductID == id).FirstOrDefault().Stock)
            {
                tmpitem.AmountInCart++;
                tmpitem.TotalCost += tmpitem.Product.Price;
                await _context.SaveChangesAsync();
            }
            else
            {
                tmpitem = new Item()
                {
                    ProductID = id,
                    ShoppingCart = ShoppingCart,
                    AmountInCart = 1,
                    TotalCost = _context.Product.Where(x => x.ProductID == id).FirstOrDefault().Price,
                    AddedToCart = DateTime.Now
                };
                await _context.Item.AddAsync(tmpitem);
                await _context.SaveChangesAsync();
                Item.Add(tmpitem);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostRemove(string val)
        {
            Product = await _context.Product.ToListAsync();
            Category = await _context.Category.Include(x => x.Products).ToListAsync();
            ShoppingCart = await _context.ShoppingCart.Where(x => x.aspNetUser.UserName == User.Identity.Name).FirstOrDefaultAsync();
            if (ShoppingCart != null)
                Item = await _context.Item.Where(x => x.ShoppingCart == ShoppingCart).ToListAsync();
            int id = int.Parse(val);
            Item tmpitem = await _context.Item.Where(x => x.ProductID == id && x.ShoppingCart == ShoppingCart).FirstOrDefaultAsync();
            if (tmpitem != null && tmpitem.AmountInCart > 0)
            {
                tmpitem.AmountInCart--;
                tmpitem.TotalCost -= tmpitem.Product.Price;
                await _context.SaveChangesAsync();
                if (tmpitem.AmountInCart == 0)
                {
                    _context.Item.Remove(tmpitem);
                    await _context.SaveChangesAsync();
                    Item.Remove(tmpitem);
                }
            }
            return Page();
        }
    }
}