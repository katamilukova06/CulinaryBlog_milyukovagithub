using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CulinaryBlog.Domain.ModelsDb
{
    [Table("recepies")]
    public class RecipesDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("category_id")]
        public Guid CategoryId { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("ingredients")]
        public string Ingredients { get; set; }

        [Column("steps")] 
        public string Steps { get; set; }

        [Column("pathImage")]
        public string PathImage { get; set; }

        [Column("time")]
        public int Time { get; set; }

    }
}
