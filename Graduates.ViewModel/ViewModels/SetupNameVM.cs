using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Graduates.ViewModel.ViewModels
{
    public class SetupNameVM
    {       
        public long Id { set; get; }
        
        public string Name { set; get; }
        [Display(Name="Parent")]
        public long? ParentId { set; get; }
     
    }
}
