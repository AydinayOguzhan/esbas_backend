using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class StudentCourseDal: EfEntityRepositoryAsyncBase<StudentCourse, EfContext>, IStudentCourseDal
    {
        public async Task<StudentCourseDetailsDto> GetStudentCoursesByStudentId(int studentId)
        {
            using (var context = new EfContext())
            {
                var result = (from studentCourse in context.StudentCourses
                             join student in context.Students
                                on studentCourse.StudentId equals student.Id
                             join course in context.Courses
                                on studentCourse.CourseId equals course.Id
                             where studentCourse.StudentId == studentId
                             select new StudentCourseDetailsDto
                             {
                                 Id = studentCourse.Id,
                                 Student = new StudentCourseDto
                                 {
                                     StudentId = student.Id,
                                     ContactNumber = student.ContactNumber,
                                     Email = student.Email,
                                     FirstName = student.FirstName,
                                     GenderId = student.GenderId,
                                     LastName = student.LastName,
                                     MaritalStatusId = student.MaritalStatusId,
                                     Username = student.Username
                                 },
                                 Courses = (from studentCourse in context.StudentCourses
                                            join student in context.Students
                                               on studentCourse.StudentId equals student.Id
                                            join course in context.Courses
                                               on studentCourse.CourseId equals course.Id
                                            where studentCourse.StudentId == studentId
                                            select new Course
                                            {
                                                Id = course.Id,
                                                Description = course.Description,
                                                Name = course.Name,
                                                Status = course.Status
                                            }).ToList()
                             }).FirstOrDefaultAsync();
                return await result;

            }
        }
    }
}
