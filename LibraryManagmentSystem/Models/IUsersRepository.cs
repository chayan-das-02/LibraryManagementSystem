using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public interface IUsersRepository
    {
        UsersModel Login(UserLoginModel user);
        bool Register(UsersModel user);
        Task<List<UsersModel>> GetAllUser();
        object Show(UsersModel user);
    }
}
