using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Models;
using FluentValidation;

namespace CulinaryBlog.Domain.Validators
{
    public class CommentsRatingValidator : AbstractValidator<CommentsRating>
    {
        public CommentsRatingValidator() 
        {
            RuleFor(comments_ratings => comments_ratings.Comment).NotEmpty().WithMessage("Комментарий обязателен");
            RuleFor(comments_ratings => comments_ratings.Rating).NotEmpty().WithMessage("Оценка рецепта обязательна")
              .MinimumLength(1).WithMessage("Оценка должна содержать не менее 1 символа");
        }
    }
}
