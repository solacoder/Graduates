using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Graduates.ViewModel.ViewModels
{
    public class SetupValueVM
    {
        public SetupValueVM()
        { 
        }
        
        public long Id { set; get; }

        [Required]
        [Display(Name ="Setup Name")]
        public long SetUpNameId { set; get; }

        [Required]
        public string Name { set; get; }

        [Display(Name ="Parent")]
        public long? ParentId { set; get; }
    }
}
