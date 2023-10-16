using Business.Abstract;
using Business.Constants;
using Core.Aspect.Autofac.Cache;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StudentCourseManager : IStudentCourseService
    {
        private IStudentCourseDal _studentCourseDal;

        public StudentCourseManager(IStudentCourseDal studentCourseDal)
        {
            _studentCourseDal = studentCourseDal;
        }

        [CacheRemoveAspect("IStudentCourseService.Get")]
        public async Task<IResult> Add(StudentCourse studentCourse)
        {
            var isDuplicatedOnUserResult = await IsCourseDuplicatedOnUser(studentCourse.StudentId, studentCourse.CourseId);
            if (isDuplicatedOnUserResult == false)
            {
                return new ErrorResult(Messages.CourseAlreadyTaken);
            }

            await _studentCourseDal.AddAsync(studentCourse);
            return new SuccessResult(Messages.Successful);
        }

        [CacheRemoveAspect("IStudentCourseService.Get")]
        public async Task<IResult> Delete(StudentCourse studentCourse)
        {
            _studentCourseDal.DeleteAsync(studentCourse);
            return new SuccessResult(Messages.Successful);
        }

        [CacheAspect]
        public async Task<IDataResult<IList<StudentCourse>>> GetAll()
        {
            return new SuccessDataResult<IList<StudentCourse>>(await _studentCourseDal.GetListAsync());
        }

        [CacheAspect]
        public async Task<IDataResult<StudentCourse>> GetById(int studentCourseId)
        {
            return new SuccessDataResult<StudentCourse>(await _studentCourseDal.GetAsync(c=>c.Id == studentCourseId));
        }

        [CacheAspect]
        public async Task<IDataResult<StudentCourseDetailsDto>> GetStudentCoursesByStudentId(int studentId)
        {
            return new SuccessDataResult<StudentCourseDetailsDto>(await _studentCourseDal.GetStudentCoursesByStudentId(studentId));
        }

        [CacheRemoveAspect("IStudentCourseService.Get")]
        public async Task<IResult> Update(StudentCourse studentCourse)
        {
            await _studentCourseDal.UpdateAsync(studentCourse);
            return new SuccessResult(Messages.Successful);
        }

        private async Task<bool> IsCourseDuplicatedOnUser(int userId, int courseId)
        {
            var result = await _studentCourseDal.GetAsync(c=>c.StudentId == userId && c.CourseId == courseId);
            if (result == null)
            {
                return true;
            }
            return false;
        }
    }
}
