using Bina.BLL.DTOs.User;
using Bina.BLL.DTOs.Common;
using Bina.BLL.Services.Contracts;
using Bina.DAL.Models;
using Bina.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bina.BLL.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterDto dto)
        {
            try
            {
                dto.Email = (dto.Email ?? string.Empty).Trim().ToLowerInvariant();
                dto.PhoneNumber = (dto.PhoneNumber ?? string.Empty).Trim();
                dto.FullName = (dto.FullName ?? string.Empty).Trim();

                var existingUsers = await _userRepository.GetAllAsync();
                if (existingUsers.Any(u => string.Equals(u.Email, dto.Email, StringComparison.OrdinalIgnoreCase)))
                    return ApiResponse<AuthResponseDto>.Fail("Bu e-poçt art?q istifad? olunur.");

                var user = new User
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Role = DAL.Enums.UserRole.User,
                    IsActive = true,
                    IsVerified = false,
                    CreatedAt = DateTime.UtcNow,
                    AvatarUrl = string.Empty
                };

                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();

                var token = GenerateJwtToken(user);

                return ApiResponse<AuthResponseDto>.Ok(new AuthResponseDto
                {
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
                    User = _mapper.Map<UserProfileDto>(user)
                });
            }
            catch (DbUpdateException)
            {
                return ApiResponse<AuthResponseDto>.Fail("Bu e-poçt art?q istifad? olunur.");
            }
            catch (Exception ex)
            {
                return ApiResponse<AuthResponseDto>.Fail("Sistem x?tas? ba? verdi.", ex.Message);
            }
        }

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto dto)
        {
            try
            {
                var normalizedEmail = (dto.Email ?? string.Empty).Trim().ToLowerInvariant();

                var users = await _userRepository.GetAllAsync();
                var user = users.FirstOrDefault(u => string.Equals(u.Email, normalizedEmail, StringComparison.OrdinalIgnoreCase));

                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                    return ApiResponse<AuthResponseDto>.Fail("E-poçt v? ya ?ifr? yanl??d?r.");

                user.LastLoginAt = DateTime.UtcNow;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                var token = GenerateJwtToken(user);

                return ApiResponse<AuthResponseDto>.Ok(new AuthResponseDto
                {
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
                    User = _mapper.Map<UserProfileDto>(user)
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<AuthResponseDto>.Fail("Sistem x?tas? ba? verdi.", ex.Message);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}