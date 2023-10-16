using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICourseService
    {
        Task<IResult> Add(Course course);
        Task<IResult> Update(Course course);
        Task<IResult> Delete(Course course);
        Task<IDataResult<IList<Course>>> GetAll();
        Task<IDataResult<Course>> GetById(int courseId);
    }
}
