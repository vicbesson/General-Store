using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prog245Final_VictorBesson.Models;
using System.ComponentModel.DataAnnotations;


namespace Prog245Final_VictorBesson.Pages.Inventory
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public readonly Prog245Final_VictorBesson.Data.ApplicationDbContext _context;

        public CheckoutModel(Prog245Final_VictorBesson.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public double Total { get; set; }
        public async Task OnGetAsync()
        {
            ReturnUrl = Url.Content("~/");
            ShoppingCart = await _context.ShoppingCart.Where(x => x.aspNetUser.UserName == User.Identity.Name).FirstOrDefaultAsync();
            if (ShoppingCart != null)
                Item = await _context.Item.Where(x => x.ShoppingCart == ShoppingCart).Include(x => x.Product).ToListAsync();
            foreach(Item i in Item)
                Total += i.TotalCost;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");
            ShoppingCart = await _context.ShoppingCart.Where(x => x.aspNetUser.UserName == User.Identity.Name).FirstOrDefaultAsync();
            if (ShoppingCart != null)
                Item = await _context.Item.Where(x => x.ShoppingCart == ShoppingCart).Include(x => x.Product).ToListAsync();
            foreach (Item i in Item)
                Total += i.TotalCost;
            if (!ModelState.IsValid)
                return Page();
            else
            {
                foreach (Item i in Item)
                {
                    Product p = await _context.Product.Where(x => x == i.Product).FirstOrDefaultAsync();
                    p.Stock -= i.AmountInCart;
                    if (p.Stock < 0)
                        p.Stock = 0;
                    await _context.SaveChangesAsync();
                    _context.Remove(i);
                }
                await _context.SaveChangesAsync();
                return LocalRedirect(ReturnUrl);
            }
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "FirstName")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "LastName")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required]
            [Display(Name = "City")]
            public string City { get; set; }

            [Required]
            [Display(Name = "Province")]
            public string Province { get; set; }

            [Required]
            [Display(Name = "Postal Code")]
            [DataType(DataType.PostalCode, ErrorMessage = "Invalid Postal Code")]
            public string PostalCode { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [Required]
            [CreditCard]
            [Display(Name = "Credit Card Number")]
            public string CreditCardNumber { get; set; }

            [Required]
            [RegularExpression(@"^(0[1-9]|1[0-2]|[1-9])\/(1[4-9]|[2-9][0-9]|20[1-9][1-9])$", ErrorMessage = "Invalid Expiry Date")]
            [Display(Name = "Expiry Date")]
            public string ExpiryDate { get; set; }

            [Required]
            [RegularExpression("^[0-9]{3,4}$", ErrorMessage = "Invalid Security Code")]
            [Display(Name = "Security Code")]
            public string SecurityCode { get; set; }
        }
    }

}