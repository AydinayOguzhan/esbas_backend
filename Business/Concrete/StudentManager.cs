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
        private IStudentDal _studentDal;

        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }

        [CacheRemoveAspect("IStudentService.Get")]
        public async Task<IResult> Add(Student user)
        {
            var result = await _studentDal.AddAsync(user);
            return new SuccessResult(Messages.Successful);
        }

        [CacheRemoveAspect("IStudentService.Get")]
        public async Task<IResult> Delete(int studentId)
        {
            var student = await _studentDal.GetAsync(s => s.Id == studentId);
            if (student == null)
            {
                return new ErrorResult(Messages.UserDoesNotExist);
            }

            _studentDal.DeleteAsync(student);
            return new SuccessResult(Messages.Successful);
        }

        [CacheAspect]
        public async Task<IDataResult<IList<Student>>> GetAll()
        {
            return new SuccessDataResult<IList<Student>>(await _studentDal.GetListAsync());
        }

        [CacheAspect]
        public async Task<IDataResult<Student>> GetByEmail(string email)
        {
            return new SuccessDataResult<Student>(await _studentDal.GetAsync(u => u.Email == email));
        }

        public async Task<IDataResult<List<OperationClaim>>> GetClaims(Student user)
        {
            return new SuccessDataResult<List<OperationClaim>>(await _studentDal.GetClaims(user));
        }

        [CacheAspect]
        public async Task<IDataResult<StudentDetailsDto>> GetStudentDetailsByStudentId(int studentId)
        {
            return new SuccessDataResult<StudentDetailsDto>(await _studentDal.GetStudentDetailsByStudentId(studentId));
        }

        [CacheRemoveAspect("IStudentService.Get")]
        public async Task<IResult> Update(Student user)
        {
            var result = await _studentDal.UpdateAsync(user);
            return new SuccessResult(Messages.Successful);
        }
    }
}
