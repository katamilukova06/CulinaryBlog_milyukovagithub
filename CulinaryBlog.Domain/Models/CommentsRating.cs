using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Enum;

namespace CulinaryBlog.Domain.Models
{
    public class CommentsRating
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RecipeId { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
    }
}
