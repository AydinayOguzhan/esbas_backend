using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStudentService
    {
        Task<IDataResult<IList<Student>>> GetAll();
        Task<IResult> Add(Student user);
        Task<IDataResult<Student>> GetByEmail(string email);
        Task<IDataResult<List<OperationClaim>>> GetClaims(Student user);
        Task<IResult> Update(Student user);
        Task<IDataResult<StudentDetailsDto>> GetStudentDetailsByStudentId(int studentId);
        Task<IResult> Delete(Student student);
    }
}
