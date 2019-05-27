using FluentValidation;
using Graduates.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.Validators
{
    public class SetupNameVMValidator : AbstractValidator<SetupNameVM>
    {
        public SetupNameVMValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Field is Required");
        }
    }
}
