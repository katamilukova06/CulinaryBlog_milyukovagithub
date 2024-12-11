using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Enum;

namespace CulinaryBlog.Domain.Models
{
    public class Categories
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PathImg { get; set; }
    }
}
