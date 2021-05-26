using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Prog245Final_VictorBesson.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemID { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int ShoppingCartID { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public int AmountInCart { get; set; }
        public double TotalCost { get; set; }
        public DateTime AddedToCart { get; set; }
    }
}
