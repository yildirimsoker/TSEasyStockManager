using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Model.Domain;
using TS.EasyStockManager.Model.Service;

namespace TS.EasyStockManager.Core.Service
{
    public interface IUserService : IService<UserDTO>
    {
        Task<ServiceResult> Login(string email, string password);
    }
}
