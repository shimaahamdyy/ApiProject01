﻿using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using Store.Services.Services.TokenServices;
using Store.Services.Services.UserServices.Dtos;

namespace Store.Services.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(SignInManager<AppUser> signInManager
                            , UserManager<AppUser> userManager
                            , ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<UserDto> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user , input.Password , false);

            if (!result.Succeeded)
                throw new Exception("Login Faild");

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.GenerateToken(user)
            };

        }

        public async Task<UserDto> Register(RegisterDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is not null)
                return null;

            var appUser = new AppUser
            {
                DisplayName = input.DisplayName,
                Email = input.Email,
                UserName = input.DisplayName,
            };

            var result = await _userManager.CreateAsync(appUser , input.Password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault());

            return new UserDto
            {
                Id = Guid.Parse(appUser.Id),
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Token = _tokenService.GenerateToken(appUser)
            };
        }

    }
}
