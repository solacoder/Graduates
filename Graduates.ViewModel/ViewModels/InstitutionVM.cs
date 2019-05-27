using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Graduates.ViewModel.ViewModels
{
    public class InstitutionVM
    {
        public long Id { set; get; }
        public String Name { set; get; }

        [Display(Name = "Year Established")]
        public String YearEstablished { set; get; }

        [Display(Name = "County")]
        public string CountyId { set; get; }
        public string CountyName { set; get; }
        public String Address { set; get; }
        public String Email { set; get; }
        public String WebSite { set; get; }
        public String Phone { set; get; }

        [Display(Name = "Ownership Type")]
        public String OwnerShipTypeId { set; get; }
        public string OwnerShipTypeName { set; get; }

        [Display(Name = "Country")]
        public String CountryId { set; get; }
        public string CountryName { set; get; }

        [Display(Name = "Type")]
        public String TypeId { set; get; }
        public string TypeName { set; get; }
    }
}
