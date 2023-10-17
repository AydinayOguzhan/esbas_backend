using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStudentDal: IEntityRepositoryAsync<Student>
    {
        public Task<List<OperationClaim>> GetClaims(Student user);
        public Task<StudentDetailsDto> GetStudentDetailsByStudentId(int studentId);
    }
}
