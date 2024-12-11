using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBlog.Domain.Models
{
    public class PictureRecipes
    {
        public Guid Id { get; set; }
        public Guid IdRecipes { get; set; }
        public string PathImg { get; set; }
    }
}
