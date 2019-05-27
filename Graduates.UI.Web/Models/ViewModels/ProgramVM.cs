using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Graduates.UI.Web.Models.ViewModels
{
    public class ProgramVM
    {
        public ProgramVM() { }
        public ProgramVM(long Id, string ProgramDurationUnitId, string ProgramDurationUnitName
                                , string QualificationTypeId, string QualificationTypeName )
        {
            this.Id = Id;
            this.ProgramDurationUnitId = ProgramDurationUnitId;
            this.ProgramDurationUnitName = ProgramDurationUnitName;
            this.QualificationTypeId = QualificationTypeId;
            this.QualificationTypeName = QualificationTypeName;
        }
        public long Id { set; get; }
        [Display(Name = "Qualification Type")]
        public string QualificationTypeId { set; get; }
        public string QualificationTypeName { set; get; }

        public string Duration { set; get; }

        [Display(Name = "Program Duration Unit")]
        public string ProgramDurationUnitId { set; get; }
        public string ProgramDurationUnitName { set; get; }
    }
}
