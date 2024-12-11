using CulinaryBlog.Domain.Enum;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBlog.Domain.Models
{
    public class Recipes
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Steps { get; set; }
        public string PathImage { get; set; }
        public int Time { get; set; }
    }
}
