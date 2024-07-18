using OrderManagementSystem.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services
{
    public interface IUserService
    {
        Task<UserDto> RegisterUserAsync(RegisterDto registerDto);
        Task<UserDto> LoginUserAsync(LoginDto loginDto);
    }
}
