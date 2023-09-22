using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager: IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IResult> Add(User user)
        {
            var result = await _userDal.AddAsync(user);
            return new SuccessResult(Messages.Successful);
        }

        public async Task<IDataResult<IList<User>>> GetAll()
        {
            return new SuccessDataResult<IList<User>>(await _userDal.GetListAsync());
        }
    }
}
