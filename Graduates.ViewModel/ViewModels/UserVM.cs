using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Graduates.ViewModel.ViewModels
{
    public class UserVM
    {
        [Required]
        public string UserName { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string FirstName { set; get; }
        [Required]
        public string LastName { set; get; }
        [Required]
        public string PhoneNumber { set; get; }
        [Required]
        public string Role { set; get; }

        public bool IsActive { set; get; }
    }
}
