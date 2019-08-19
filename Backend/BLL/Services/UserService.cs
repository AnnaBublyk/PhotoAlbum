using BLL.DTO;
using BLL.Interface;
using DAL.DataModel;
using DAL.Interfaces;
using System.Linq;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BLL.Services
{
    /// <summary>Class UserService.
    /// Implements the <see cref="BLL.Interface.IUserService"/></summary>
    /// <seealso cref="BLL.Interface.IUserService" />
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:BLL.Services.UserService"/> class.</summary>
        /// <param name="uow">The uow.</param>
        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public static string userName = "UserName";
        public static string role = "Role";
        public static string isBlocked = "IsBlocked";
        public static string userId = "UserId";
        /// <summary>Creates the specified user.</summary>
        /// <param name="userDto">The user.</param>
        public void Create(UserDTO userDto)
        {
            User user =  Database.UserManager.FindByName(userDto.UserName);
            if (user == null)
            {
                user = new User { UserName = userDto.UserName };
                var result =  Database.UserManager.Create(user, userDto.Password);
                // добавляем роль
                 Database.UserManager.AddToRole(user.Id, userDto.Role);
                // создаем профиль клиента
                Profile clientProfile = new Profile { ProfileId = user.Id, Role = userDto.Role, UserName = userDto.UserName, FirstName=userDto.FirstName, LastName=userDto.LastName };
                 Database.Profiles.Create(clientProfile);
                 Database.Save();
            }
        }
        /// <summary>Gets the JWT.</summary>
        /// <param name="userDto">The user.</param>
        /// <returns>dynamic.</returns>
        public dynamic GetJwt(UserDTO userDto)
        {
            var identity = GetIdentity(userDto);
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                 issuer: "myserver",
                    audience: "http://localhost:4200/",
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(1))

                    );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = userDto.UserName
            };
            return response;
        }
        /// <summary>Authenticates the specified user.</summary>
        /// <param name="userDto">The user.</param>
        /// <returns>ClaimsIdentity.</returns>
        public ClaimsIdentity Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            PasswordVerificationResult result = PasswordVerificationResult.Failed;
            User user = Database.UserManager.FindByName(userDto.UserName);
            if (user != null)
            {
                result = Database.UserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, userDto.Password);
            }
            if ((result == PasswordVerificationResult.Success))
            {
                claim = Database.UserManager.CreateIdentity(user,
                                             DefaultAuthenticationTypes.ApplicationCookie);
            }
           
            return claim;
        }

        /// <summary>Gets the identity. Gets information about user</summary>
        /// <param name="userDto">The user.</param>
        /// <returns>ClaimsIdentity.</returns>
        private ClaimsIdentity GetIdentity(UserDTO userDto)
        {
            User user = Database.UserManager.FindByName(userDto.UserName);
            Profile profile = Database.Profiles.GetAll().FirstOrDefault(u=>u.ProfileId==user.Id);
            if (profile != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(userName, profile.UserName),
                    new Claim(role, profile.Role),
                    new Claim(isBlocked, Convert.ToInt32(profile.IsBlocked).ToString()),
                    new Claim(userId, profile.ProfileId)

                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        /// <summary>Sets the initial data.</summary>
        /// <param name="adminDto">The admin.</param>
        /// <param name="roles">The roles.</param>
        public void SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role =  Database.RoleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new Role { Name = roleName };
                    Database.RoleManager.Create(role);
                }
            }
             Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
