using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Graduates.UI.Web.Models.ViewModels
{
    public class InstitutionVM
    {
        public Int64 Id { set; get; }
        public String Name { set; get; }

        [Display(Name = "Year Established")]
        public String YearEstablished { set; get; }

        [Display(Name = "County")]
        public int CountyId { set; get; }
        public String Address { set; get; }
        public String Email { set; get; }
        public String WebSite { set; get; }
        public String Phone { set; get; }

        [Display(Name = "Ownership Type")]
        public String OwnerShipTypeId { set; get; }

        [Display(Name = "Country")]
        public String CountryId { set; get; }

        [Display(Name = "Type")]
        public String TypeId { set; get; }
    }
}
