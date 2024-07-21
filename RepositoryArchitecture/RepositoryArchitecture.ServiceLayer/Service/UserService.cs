using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryArchitecture.DataLayer.IRepository;
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

            //_userData.CreateUser(userModel);
            throw new NotImplementedException();
        }

        public void GetUsers()
        {
            _userData.GetUsers();
        }
    }
}
