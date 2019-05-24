using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Category
    {
        //[Column("Category_Id")]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int? ParentCategoryId { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }
    }
}
