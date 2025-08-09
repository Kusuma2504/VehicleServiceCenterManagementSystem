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

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _authRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthResponseDto { Success = false, Message = "Email already registered" };
            }

            var hashedPassword = _passwordHasher.HashPassword(request.Password);
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = hashedPassword,
                UserType = Enum.Parse<UserType>(request.UserType, true)
            };

            await _authRepository.CreateUserAsync(user);
            return new AuthResponseDto { Success = true, Message = "Registration successful" };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _authRepository.GetUserByEmailAsync(request.Email);
            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return new AuthResponseDto { Success = false, Message = "Invalid email or password" };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthResponseDto { Success = true, Message = "Login successful", Token = token };
        }
    }
}
