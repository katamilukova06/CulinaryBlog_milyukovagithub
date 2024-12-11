using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBlog.Domain.ViewModels
{
    public class CategoriesViewModel
    {
        public string Name { get; set; }
        public string PathImg { get; set; }
        public Guid Id { get; set; }
    }
}
