using FluentValidation;
using Graduates.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.Validators
{
    class DepartmentVMValidator : AbstractValidator<DepartmentVM>
    {
        public DepartmentVMValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.FacultyId).NotEmpty();
        }
    }
}
