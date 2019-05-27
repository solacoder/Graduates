using FluentValidation;
using Graduates.UI.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.UI.Web.Models.Validators
{
    public class SetupNameVMValidator : AbstractValidator<SetupNameVM>
    {
        public SetupNameVMValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Field is Required");
        }
    }
}
