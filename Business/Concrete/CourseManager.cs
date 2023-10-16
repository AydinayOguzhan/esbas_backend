using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Cache;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CourseManager : ICourseService
    {
        private ICourseDal _courseDal;

        public CourseManager(ICourseDal courseDal)
        {
            _courseDal = courseDal;
        }

        [ValidationAspect(typeof(CourseValidator))]
        [CacheRemoveAspect("ICourseService.Get")]
        public async Task<IResult> Add(Course course)
        {
            var duplicateResult = await IsCourseDuplicated(course.Name);
            if (duplicateResult == true)
            {
                return new ErrorResult(Messages.CourseAlreadyExists);
            }

            await _courseDal.AddAsync(course);
            return new SuccessResult(Messages.Successful);
        }

        [ValidationAspect(typeof(CourseValidator))]
        [CacheRemoveAspect("ICourseService.Get")]
        public async Task<IResult> Delete(Course course)
        {
            _courseDal.DeleteAsync(course);
            return new SuccessResult(Messages.Successful);
        }

        [CacheAspect]
        public async Task<IDataResult<IList<Course>>> GetAll()
        {
            return new SuccessDataResult<IList<Course>>(await _courseDal.GetListAsync());
        }

        [CacheAspect]
        public async Task<IDataResult<Course>> GetById(int courseId)
        {
            return new SuccessDataResult<Course>(await _courseDal.GetAsync(c=>c.Id == courseId));
        }

        [ValidationAspect(typeof(CourseValidator))]
        [CacheRemoveAspect("ICourseService.Get")]
        public async Task<IResult> Update(Course course)
        {
            await _courseDal.UpdateAsync(course);
            return new SuccessResult(Messages.Successful);
        }

        private async Task<bool> IsCourseDuplicated(string courseName)
        {
            var result = await _courseDal.GetAsync(c=>c.Name == courseName);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
