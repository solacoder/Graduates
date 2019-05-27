using Graduates.Service.Abstract;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Graduates.Service.Concrete;

namespace Graduates.UI.Web.Models.ViewModels
{
    public class SetupNameVM
    {
        ISetupNameService _setupNameService;

        public SetupNameVM()
        {
        }
       
        public long Id { set; get; }
        
        public string Name { set; get; }
        [Display(Name="Parent")]
        public long? ParentId { set; get; }
        public SelectList SetupNames { set; get; }
        
    }

}
