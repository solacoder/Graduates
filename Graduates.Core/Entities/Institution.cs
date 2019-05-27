using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("Institution")]
    public class Institution
    {
        public long Id { set; get; }
        public String Name { set; get; }
        public String YearEstablished { set; get; }
        
        public String Address { set; get; }
        public String Email { set; get; }
        public String WebSite { set; get; }
        public String Phone { set; get; }

        public long? OwnershipTypeId { set; get; }
        public virtual SetupValue OwnershipType { set; get; }

        public long? CountryId { set; get; }
        public virtual SetupValue Country { set; get; }

        public long? CountyId { set; get; }
        public virtual SetupValue County { set; get; }

        public long? TypeId { set; get; }
        public virtual SetupValue Type { set; get; }

        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
