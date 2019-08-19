using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BLL.Interface
{
    public interface IUserService : IDisposable
    {
        void Create(UserDTO userDto);
        ClaimsIdentity Authenticate(UserDTO userDto);
        dynamic GetJwt(UserDTO userDto);
        void SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
