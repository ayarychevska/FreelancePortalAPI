using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Models.ApplicationUsers;
using Services.Models.Login;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Services.Services.ApplicationUsers
{
    public class ApplicationUsersService : BaseService<ApplicationUser>
    {
        private readonly IConfiguration Configuration;
        private UsersSubjectsService UsersSubjectsService;
        protected readonly IHttpContextAccessor _httpContextAccessor;


        public ApplicationUsersService(IRepository<ApplicationUser> repository,
                IHttpContextAccessor httpContextAccessor,
                IMapper mapper,
                IConfiguration configuration,
                UsersSubjectsService usersSubjectsService
            ) : base(repository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            Configuration = configuration;
            UsersSubjectsService = usersSubjectsService;
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
            var baseEntity = Repository.FindQuery(x => x.Id == createModel.Id).Include(x => x.UsersSubjects).SingleOrDefault();
            ApplicationUser applicationUser = Mapper.Map(createModel, baseEntity);

            UsersSubjectsService.EraseUserSubjects(createModel.Id);
            UsersSubjectsService.AddUserSubjects(createModel.Id, createModel.SubjectsIds.ToArray());

            var result = Repository.Update(applicationUser);
            return result;
        }

        //I want to die
        public ApplicationUser GetUserById(string id)
        {
            var user = Repository.FindQuery(x => x.Id == id).Include(x => x.UsersSubjects).ThenInclude(x => x.Subject).SingleOrDefault();
            if (user == null)
                throw new NullReferenceException();
            return user;
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
                new Claim(ClaimTypes.Actor, userInfo.Id),
                new Claim("fullName", userInfo.UserName.ToString()),
                new Claim(ClaimTypes.Role, userInfo.UserType),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(300),
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

        public bool IsTeacher()
        {
            if (_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(claims => claims.Type == ClaimTypes.Role).Value == "student")
                return false;
            if (_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(claims => claims.Type == ClaimTypes.Role).Value == "teacher")
                return true;
            else
                throw new NullReferenceException();
        }
    }
}
