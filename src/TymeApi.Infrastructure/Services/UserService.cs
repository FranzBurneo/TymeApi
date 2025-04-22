using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TymeApi.Application.DTOs;
using TymeApi.Application.Interfaces;
using TymeApi.Domain.Entities;
using TymeApi.Persistence;

namespace TymeApi.Infrastructure.Services
{
    public class UserService(TymeDbContext context) : IUserService
    {
        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            return await context.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt
                }).ToListAsync();
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<int> CreateAsync(UserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<bool> UpdateAsync(int id, UserDto dto)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return false;

            user.Username = dto.Username;
            user.Email = dto.Email;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return false;

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }


        public async Task<int> CreateWithStoredProcedureAsync(UserDto dto)
        {
            var usernameParam = new SqlParameter("@Username", dto.Username);
            var emailParam = new SqlParameter("@Email", dto.Email);

            return await context.Database.ExecuteSqlRawAsync(
                "EXEC dbo.InsertUser @Username, @Email",
                usernameParam, emailParam);
        }

    }
}
