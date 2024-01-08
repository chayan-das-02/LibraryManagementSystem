using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class UsersRepository : IUsersRepository
    {
        ProjectDbContext _projectDbContext;
        public UsersRepository(ProjectDbContext projectDbContext)
        {
            this._projectDbContext = projectDbContext;
        }

        public async Task<List<UsersModel>> GetAllUser()
        {
            return await _projectDbContext.UsersTable.Include(nameof(RoleModel)).ToListAsync();
        }

        public UsersModel Login(UserLoginModel login)
        {
            var data = _projectDbContext.UsersTable.Where(e => e.user_name == login.user_name).SingleOrDefault();
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public bool Register(UsersModel registration)
        {
            var data = _projectDbContext.UsersTable.SingleOrDefault(e => e.user_name == registration.user_name);
            if (data == null)
            {
                
                _projectDbContext.Add(registration);
                registration.role_id = 2;
                _projectDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public object Show(UsersModel user)
        {
            //return user.role.role_name;
            return null;
        }
    }
}
