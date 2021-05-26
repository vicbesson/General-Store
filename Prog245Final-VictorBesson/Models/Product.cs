using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Prog245Final_VictorBesson.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public DateTime Created { get; set; }
        public virtual IList<Item> Items { get; set; }
    }
}
