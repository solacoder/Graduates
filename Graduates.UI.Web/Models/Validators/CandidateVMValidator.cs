using FluentValidation;
using Graduates.UI.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.UI.Web.Models.Validators
{
    class CandidateVMValidator : AbstractValidator<CandidateVM>
    {
        public CandidateVMValidator()
        {
            RuleFor(x => x.CandidateNo).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.FirstName).NotEmpty().Length(50);
            RuleFor(x => x.LastName).NotEmpty().Length(50);
            RuleFor(x => x.SexId).NotEmpty();
            RuleFor(x => x.DateOfBirth).Empty();
        }
    }
}
