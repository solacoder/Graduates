using FluentValidation;
using Graduates.UI.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.UI.Web.Models.Validators
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
