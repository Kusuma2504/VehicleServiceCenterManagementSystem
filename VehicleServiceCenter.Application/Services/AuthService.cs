using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleServiceCenter.Application.DTOs.Auth;
using VehicleServiceCenter.Application.Interfaces;
using VehicleServiceCenter.Domain.Entities;
using VehicleServiceCenter.Infrastructure.Repositories;
using VehicleServiceCenter.Infrastructure.Security;
using VehicleServiceCenter.Domain.Enums;

namespace VehicleServiceCenter.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepository _authRepository;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AuthRepository authRepository, PasswordHasher passwordHasher, JwtTokenGenerator jwtTokenGenerator)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthRegisterDto> RegisterAsync(RegisterRequestDto request)
        {
            var result = new AuthRegisterDto();

            var existingUser = await _authRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                result.Success = false;
                result.Message = "Email already registered";
            }
            else
            {
                var hashedPassword = _passwordHasher.HashPassword(request.Password);
                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = hashedPassword,
                    UserType = Enum.Parse<UserType>(request.UserType, true)
                };

                await _authRepository.CreateUserAsync(user);
                result.Success = true;
                result.Message = "Registration successful";
            }

            return result;
        }


        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _authRepository.GetUserByEmailAsync(request.Username);
            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return new AuthResponseDto { Success = false };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthResponseDto { Success = true, Token = token };
        }
    }
}
