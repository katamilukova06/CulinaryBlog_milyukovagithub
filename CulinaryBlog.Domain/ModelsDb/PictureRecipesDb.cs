using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBlog.Domain.ModelsDb
{
    [Table("pictures_recipes")]
    public class PictureRecipesDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("id_recipes")]
        public Guid  IdRecipes{ get; set; }

        [Column("path_img")]
        public string PathImg { get; set; }
    }
}
