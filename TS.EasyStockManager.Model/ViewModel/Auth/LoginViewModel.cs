using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        [Required]
        [MaxLength(50)]
        [Display]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(18)]
        [Display]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
