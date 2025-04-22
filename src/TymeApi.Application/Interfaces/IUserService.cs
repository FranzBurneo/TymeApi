using TymeApi.Application.DTOs;

namespace TymeApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(UserDto dto);
        Task<bool> UpdateAsync(int id, UserDto dto);
        Task<bool> DeleteAsync(int id);

        Task<int> CreateWithStoredProcedureAsync(UserDto dto);
    }
}
