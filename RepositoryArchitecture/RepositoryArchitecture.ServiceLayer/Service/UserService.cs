using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryArchitecture.DataLayer.IRepository;
using RepositoryArchitecture.DataLayer.Models;
using RepositoryArchitecture.Model.Models;
using RepositoryArchitecture.ServiceLayer.IService;

namespace RepositoryArchitecture.ServiceLayer.Service
{
    public class UserService : IUserService
    {
        public readonly IUserData _userData;

        public UserService(IUserData userData)
        {
            _userData = userData;
        }

        public void CreateUser(UserModel userModel)
        {
            Users uObj = new Users();
            uObj.UserName = userModel.UserName;
            uObj.UserId = userModel.UserId;
            uObj.UserAge = userModel.UserAge;

            _userData.CreateUser(uObj);
        }

        public void GetUsers()
        {
            var users = _userData.GetUsers();

            //return users;
        }
    }
}
