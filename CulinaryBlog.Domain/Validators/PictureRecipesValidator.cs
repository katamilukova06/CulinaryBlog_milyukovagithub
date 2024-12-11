using CulinaryBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CulinaryBlog.Domain.Validators
{
    public class PictureRecipesValidator : AbstractValidator<PictureRecipes>
    {
        public PictureRecipesValidator()
        {
            RuleFor(picture => picture.IdRecipes).NotEmpty().WithMessage("Id рецепта обязательно");
            RuleFor(picture => picture.PathImg).NotEmpty().WithMessage("Путь изображения обязателен");
        }
    }
}
