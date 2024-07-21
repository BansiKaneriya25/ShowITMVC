using RepositoryArchitecture.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryArchitecture.ServiceLayer.IService
{
    public interface IUserService
    {
        void CreateUser(UserModel userModel);
        void GetUsers();
    }
}
