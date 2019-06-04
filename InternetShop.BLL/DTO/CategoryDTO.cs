using System;
using System.Collections.Generic;
using System.Text;

namespace InternetShop.BLL.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
