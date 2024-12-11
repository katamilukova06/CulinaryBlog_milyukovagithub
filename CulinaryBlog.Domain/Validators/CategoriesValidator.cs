using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using CulinaryBlog.Domain.Validators;
using CulinaryBlog.Domain.Models;

namespace CulinaryBlog.Domain.Validators
{
    public class CategoriesValidator : AbstractValidator<Categories>
    {
        public CategoriesValidator() 
        {
            RuleFor(categories => categories.Name).NotEmpty().WithMessage("Название категории обязательно");
            RuleFor(categories => categories.PathImg).NotEmpty().WithMessage("Путь к картинке обязателен");
        }
    }
}
