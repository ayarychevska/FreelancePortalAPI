using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Models.ApplicationUsers;
using Services.Models.Login;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Services.ApplicationUsers
{
    public class ApplicationUsersService : BaseService<ApplicationUser>
    {
        private readonly IConfiguration Configuration;

        public ApplicationUsersService(IRepository<ApplicationUser> repository, IMapper mapper, IConfiguration configuration) : base(repository, mapper)
        {
            Configuration = configuration;
        }

        public ApplicationUser Create(CreateModel createModel)
        {
            ApplicationUser applicationUser = Mapper.Map<ApplicationUser>(createModel);

            applicationUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createModel.Password);

            var result = Repository.Add(applicationUser);
            return result;
        }

        public ApplicationUser Update(CreateModel createModel)
        {
            var baseEntity = Repository.GetSingleOrDefault(x => x.Id == createModel.Id); 
            ApplicationUser applicationUser = Mapper.Map(createModel, baseEntity);

            var result = Repository.Update(applicationUser);
            return result;
        }

        public ApplicationUser ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var baseEntity = Repository.GetSingleOrDefault(x => x.Id == changePasswordModel.Id);

            if (!BCrypt.Net.BCrypt.Verify(changePasswordModel.PreviousPassword, baseEntity.PasswordHash))
                throw new ArgumentException();

            baseEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordModel.Password);

            var result = Repository.Update(baseEntity);
            return result;
        }

        public string GenerateJWTToken(ApplicationUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("fullName", userInfo.UserName.ToString()),
                new Claim("role", userInfo.UserType),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ApplicationUser AuthenticateUser(UserModel loginCredentials)
        {
            var user = Repository.GetSingleOrDefault(x => x.Login == loginCredentials.Login);

            if (user == null)
            {
                throw new NullReferenceException();
            }
            else if (!BCrypt.Net.BCrypt.Verify(loginCredentials.Password, user.PasswordHash))
            {
                throw new ArgumentException();
            }

            return user;
        }
    }
}
