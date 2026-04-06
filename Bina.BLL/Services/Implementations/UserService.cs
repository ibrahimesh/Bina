using Bina.BLL.DTOs.User;
using Bina.BLL.DTOs.Common;
using Bina.BLL.Services.Contracts;
using Bina.DAL.Repositories.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace Bina.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UserProfileDto>> GetProfileAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null) return ApiResponse<UserProfileDto>.Fail("?stifad?çi tap?lmad?");

                return ApiResponse<UserProfileDto>.Ok(_mapper.Map<UserProfileDto>(user));
            }
            catch (System.Exception ex)
            {
                return ApiResponse<UserProfileDto>.Fail("X?ta ba? verdi", ex.Message);
            }
        }

        public async Task<ApiResponse<UserProfileDto>> UpdateProfileAsync(int userId, UpdateProfileDto dto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null) return ApiResponse<UserProfileDto>.Fail("?stifad?çi tap?lmad?");

                _mapper.Map(dto, user);
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                return ApiResponse<UserProfileDto>.Ok(_mapper.Map<UserProfileDto>(user));
            }
            catch (System.Exception ex)
            {
                return ApiResponse<UserProfileDto>.Fail("Sistem x?tas?", ex.Message);
            }
        }
    }
}