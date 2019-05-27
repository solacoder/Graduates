using FluentValidation;
using Graduates.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.Validators
{
    public class SetupValueVMValidator : AbstractValidator<SetupValueVM>
    {
        public SetupValueVMValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("*");
        }
    }
}
