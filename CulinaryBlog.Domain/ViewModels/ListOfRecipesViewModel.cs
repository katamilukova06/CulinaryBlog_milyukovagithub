using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBlog.Domain.ViewModels
{
    public class ListOfRecipesViewModel
    {
        public List<RecipesForListOfRecipesViewModel> Recipes { get; set; }
        public Guid CategoryId { get; set; }
    }
    public class RecipesForListOfRecipesViewModel
    {
        public Guid Id { get; set;}
        public Guid UserId { get; set; }

        public string Title { get; set; }

        public Guid CategoryId { get; set; }

        public string Description { get; set; }

        public string Ingredients { get; set; }

        public string Steps { get; set; }

        public string PathImage { get; set; }

        public DateInterval Time { get; set; }

    }
}
