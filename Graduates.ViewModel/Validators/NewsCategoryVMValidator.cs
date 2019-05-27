using FluentValidation;
using Graduates.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.Validators
{
    public class NewsCategoryVMValidator : AbstractValidator<NewsCategoryVM>
    {
        public NewsCategoryVMValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Field is Required");
        }
    }
}
