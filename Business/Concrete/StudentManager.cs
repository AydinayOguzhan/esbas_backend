using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Cache;
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
    public class StudentManager: IStudentService
    {
        private IStudentDal _userDal;

        public StudentManager(IStudentDal userDal)
        {
            _userDal = userDal;
        }

        [CacheRemoveAspect("IStudentService.Get")]
        public async Task<IResult> Add(Student user)
        {
            var result = await _userDal.AddAsync(user);
            return new SuccessResult(Messages.Successful);
        }

        [CacheAspect]
        public async Task<IDataResult<IList<Student>>> GetAll()
        {
            return new SuccessDataResult<IList<Student>>(await _userDal.GetListAsync());
        }

        [CacheAspect]
        public async Task<IDataResult<Student>> GetByEmail(string email)
        {
            return new SuccessDataResult<Student>(await _userDal.GetAsync(u => u.Email == email));
        }

        public async Task<IDataResult<List<OperationClaim>>> GetClaims(Student user)
        {
            return new SuccessDataResult<List<OperationClaim>>(await _userDal.GetClaims(user));
        }

        [CacheRemoveAspect("IStudentService.Get")]
        public async Task<IResult> Update(Student user)
        {
            var result = await _userDal.UpdateAsync(user);
            return new SuccessResult(Messages.Successful);
        }
    }
}
