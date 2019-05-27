using FluentValidation;
using Graduates.UI.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.UI.Web.Models.Validators
{
    class CourseVMValidator : AbstractValidator<CourseVM>
    {
        public CourseVMValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(100);
            RuleFor(x => x.InstitutionId).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
