using RepositoryArchitecture.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryArchitecture.DataLayer.IRepository
{
    public interface IUserData
    {
        void CreateUser(Users users);
        void GetUsers();
    }
}
