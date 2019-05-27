using FluentValidation;
using Graduates.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.Validators
{
    class RoleVMValidator : AbstractValidator<RoleVM>
    {
        public RoleVMValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(100);
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
