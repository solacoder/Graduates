using FluentValidation;
using Graduates.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.Validators
{
    public class NewsVMValidator : AbstractValidator<NewsVM>
    {
        public NewsVMValidator()
        {
            RuleFor(m => m.Title).NotEmpty().WithMessage("Field is Required");
            RuleFor(m => m.SubTitle).NotEmpty().WithMessage("Field is Required");
            RuleFor(m => m.NewsCategoryId).NotEmpty().WithMessage("Field is Required");
        }
    }
}
