using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.User
{
    public class ListUserViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
