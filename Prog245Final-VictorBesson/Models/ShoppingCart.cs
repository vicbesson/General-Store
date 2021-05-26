using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Prog245Final_VictorBesson.Models
{
    public class ShoppingCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShoppingCartID { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual IdentityUser aspNetUser { get; set; }
        public DateTime Created { get; set; }
        public virtual IList<Item> Items { get; set; }
    }
}
