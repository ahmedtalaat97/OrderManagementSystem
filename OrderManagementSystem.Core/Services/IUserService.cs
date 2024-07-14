using OrderManagementSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task RegisterAsync(User user, string password);
    }
}
