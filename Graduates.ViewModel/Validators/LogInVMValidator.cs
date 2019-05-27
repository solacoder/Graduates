using FluentValidation;
using Graduates.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.Validators
{
    public class LogInVMValidator : AbstractValidator<LogInVM>
    {
        public LogInVMValidator()
        {
            RuleFor(m => m.UserName).NotEmpty().WithMessage("Username is Required");
            RuleFor(m => m.Password).NotEmpty().WithMessage("Password is Required");
        }
    }
}
