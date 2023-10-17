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
        private IStudentService _studentService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IStudentService userService, ITokenHelper tokenHelper)
        {
            _studentService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(Student user)
        {
            var claims = await _studentService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.Successful);
        }

        public async Task<IDataResult<Student>> Login(LoginDto loginDto)
        {
            var userToCheck = await _studentService.GetByEmail(loginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<Student>(Messages.UserDoesNotExist);
            }

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<Student>(Messages.PasswordError);
            }

            return new SuccessDataResult<Student>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        [ValidationAspect(typeof(RegisterValidator))]
        public async Task<IResult> Register(RegisterDto registerDto)
        {
            byte[] passwordSalt, passwordHash;
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);
            Student user = new Student
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Status = true,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                ContactNumber = registerDto.ContactNumber,
                GenderId = registerDto.GenderId,
                MaritalStatusId = registerDto.MaritalStatusId,
                Username = registerDto.Username
            };
            var result = await _studentService.Add(user);
            return new SuccessResult(Messages.Successful);
        }

        [ValidationAspect(typeof(RegisterValidator))]
        public async Task<IResult> UpdateUser(StudentUpdateDto studentUpdateDto)
        {
            var userToCheck = await _studentService.GetByEmail(studentUpdateDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<Student>(Messages.UserDoesNotExist);
            }

            Student user = new Student
            {
                Id = userToCheck.Data.Id,
                Email = studentUpdateDto.Email,
                FirstName = studentUpdateDto.FirstName,
                LastName = studentUpdateDto.LastName,
                Status = true,
                PasswordHash = userToCheck.Data.PasswordHash,
                PasswordSalt = userToCheck.Data.PasswordSalt,
                ContactNumber = studentUpdateDto.ContactNumber,
                GenderId = studentUpdateDto.GenderId,
                MaritalStatusId = studentUpdateDto.MaritalStatusId,
                Username = studentUpdateDto.Username
            };
            var result = await _studentService.Update(user);
            return new SuccessResult(Messages.Successful);
        }

        public async Task<IResult> UserExists(string email)
        {
            var result = await _studentService.GetByEmail(email);
            if (result.Data == null) return new SuccessResult(Messages.UserDoesNotExist);
            else return new ErrorResult(Messages.UserAlreadyExists);
        }
    }
}
