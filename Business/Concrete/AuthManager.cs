using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
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

        public AuthManager(IUserService userService)
        {
            _userService = userService;
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
    }
}
