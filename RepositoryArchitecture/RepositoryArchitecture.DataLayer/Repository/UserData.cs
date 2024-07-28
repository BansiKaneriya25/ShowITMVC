using RepositoryArchitecture.DataLayer.IRepository;
using RepositoryArchitecture.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryArchitecture.DataLayer.Repository
{
    public class UserData : IUserData
    {
        public void CreateUser(Users users)
        {
            //all database operations
        }

        public List<Users> GetUsers()
        {
            return new List<Users>();
        }
    }
}
