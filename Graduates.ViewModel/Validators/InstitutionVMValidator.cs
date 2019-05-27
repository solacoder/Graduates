using FluentValidation;
using Graduates.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.Validators
{
    class InstitutionVMValidator : AbstractValidator<InstitutionVM>
    {
        public InstitutionVMValidator()
        {
            RuleFor(x => x.CountryId).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.YearEstablished).NotEmpty();
            RuleFor(x => x.TypeId).NotNull();
        }
    }
}
