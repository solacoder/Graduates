using FluentValidation;
using Graduates.UI.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.UI.Web.Models.Validators
{
    class CandidateCertificateVMValidator : AbstractValidator<CandidateCertificateVM>
    {
        public CandidateCertificateVMValidator()
        {
            RuleFor(x => x.CertificateNo).NotNull();
            RuleFor(x => x.ProgramId).NotEmpty();
            RuleFor(x => x.StudentNo).NotEmpty();
            RuleFor(x => x.InstitutionId).NotNull();
            RuleFor(x => x.YearObtained).NotNull();
        }
    }
}
