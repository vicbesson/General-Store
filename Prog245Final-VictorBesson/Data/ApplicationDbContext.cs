using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Prog245Final_VictorBesson.Models;

namespace Prog245Final_VictorBesson.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Prog245Final_VictorBesson.Models.Product> Product { get; set; }
        public DbSet<Prog245Final_VictorBesson.Models.Category> Category { get; set; }
        public DbSet<Prog245Final_VictorBesson.Models.Item> Item { get; set; }
        public DbSet<Prog245Final_VictorBesson.Models.ShoppingCart> ShoppingCart { get; set; }
    }
}
