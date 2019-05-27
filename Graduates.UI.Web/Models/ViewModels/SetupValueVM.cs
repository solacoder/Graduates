using Graduates.Service.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Graduates.UI.Web.Models.ViewModels
{
    public class SetupValueVM
    {
        ISetupValueService _setupValueService;

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
