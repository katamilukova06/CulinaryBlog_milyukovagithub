using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CulinaryBlog.Domain.ModelsDb
{
    [Table("categories")]
    public class CategoriesDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("pathimg")] 
        public string PathImg { get; set; }
    }
}
