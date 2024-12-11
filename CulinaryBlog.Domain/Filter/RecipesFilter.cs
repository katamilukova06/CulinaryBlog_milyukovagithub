using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBlog.Domain.Filter
{
    public class RecipesFilter
    {
        public Guid IdCategory { get; set; }
        public decimal TimeMin { get; set; }
        public decimal TimeMax { get; set; }
    }
}
