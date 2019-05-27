using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("Program")]
    public class Program
    {
        public long Id { set; get; }
        public long QualificationTypeId { set; get; }
        public virtual SetupValue QualificationType { set; get; }

        public int Duration { set; get; }

        public long ProgramDurationUnitId { set; get; }
        public virtual SetupValue ProgramDurationUnit { set; get; }

        public long CourseId { set; get; }
        public virtual Course Course { set; get; }

    }
}
