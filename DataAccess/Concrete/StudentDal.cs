using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class StudentDal : EfEntityRepositoryAsyncBase<Student, EfContext>, IStudentDal
    {
        public async Task<List<OperationClaim>> GetClaims(Student user)
        {
            using (var context = new EfContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name, Status = operationClaim.Status };
                return await result.ToListAsync();

            }
        }

        public async Task<StudentDetailsDto> GetStudentDetailsByStudentId(int studentId)
        {
            using (var context = new EfContext())
            {
                var result = from student in context.Students
                             join gender in context.Genders
                                on student.GenderId equals gender.Id
                             join maritalStatus in context.MaritalStatuses
                                on student.MaritalStatusId equals maritalStatus.Id
                             select new StudentDetailsDto
                             {
                                 ContactNumber = student.ContactNumber,
                                 Username = student.Username,
                                 Email = student.Email,
                                 FirstName = student.FirstName,
                                 LastName = student.LastName,
                                 GenderId = student.GenderId,
                                 GenderName = gender.Name,
                                 MaritalStatusId = student.MaritalStatusId,
                                 MaritalStatusName = maritalStatus.Name
                             };
                return await result.FirstOrDefaultAsync();
            }
        }
    }
}
