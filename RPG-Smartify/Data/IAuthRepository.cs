using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.Data
{
   public interface IAuthRepository
    {
        Task<ResponseData<int>> Register(User user, string password);

        Task<ResponseData<string>> Login(string username, string password);

        Task<bool> UserExist(string username);

    }
}
