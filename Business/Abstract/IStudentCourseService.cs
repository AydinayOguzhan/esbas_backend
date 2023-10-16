using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStudentCourseService
    {
        Task<IResult> Add(StudentCourse studentCourse);
        Task<IResult> Update(StudentCourse studentCourse);
        Task<IResult> Delete(StudentCourse studentCourse);
        Task<IDataResult<IList<StudentCourse>>> GetAll();
        Task<IDataResult<StudentCourse>> GetById(int studentCourseId);

        Task<IDataResult<StudentCourseDetailsDto>> GetStudentCoursesByStudentId(int studentId);
    }
}
