using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulinaryBlog.Domain.ModelsDb
{
    [Table("comments_ratings")]
    public class CommentsRatingDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("recipe_id")]
        public Guid RecipeId { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("rating")]
        public string Rating { get; set; }
    }
}
