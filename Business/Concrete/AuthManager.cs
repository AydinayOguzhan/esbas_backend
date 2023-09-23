using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var claims = await _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.Successful);
        }

        public async Task<IDataResult<User>> Login(LoginDto loginDto)
        {
            var userToCheck = await _userService.GetByEmail(loginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserDoesNotExist);
            }

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        [ValidationAspect(typeof(RegisterValidator))]
        public async Task<IResult> Register(RegisterDto registerDto)
        {
            byte[] passwordSalt, passwordHash;
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);
            User user = new User
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Status = true,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            var result = await _userService.Add(user);
            return new SuccessResult(Messages.Successful);
        }

        public async Task<IResult> UserExists(string email)
        {
            var result = await _userService.GetByEmail(email);
            if (result.Data == null) return new SuccessResult(Messages.UserDoesNotExist);
            else return new ErrorResult(Messages.UserAlreadyExists);
        }
    }
}
