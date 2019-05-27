using FluentValidation;
using Graduates.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.Validators
{
    class FacultyVMValidator : AbstractValidator<FacultyVM>
    {
        public FacultyVMValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.InstitutionId).NotEmpty();
        }
    }
}
