using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using CulinaryBlog.Domain.Models;

namespace CulinaryBlog.Domain.Validators
{
    public class RecipesValidator : AbstractValidator<Recipes>
    {
        public RecipesValidator() 
        {
            RuleFor(recipies => recipies.Title).NotEmpty().WithMessage("Название рецепта обязательно");
            RuleFor(recipies => recipies.Description).NotEmpty().WithMessage("Описание рецепта обязательно");
            RuleFor(recipies => recipies.Ingredients).NotEmpty().WithMessage("Список ингредиентов обязателен");
            RuleFor(recipies => recipies.Steps).NotEmpty().WithMessage("Шаги приготовления обязательны");
            RuleFor(recipies => recipies.PathImage).NotEmpty().WithMessage("Путь изображения обязателен");
            RuleFor(recipies => recipies.Time).NotEmpty().WithMessage("Время приготовления обязательно");
        }
    }
}
