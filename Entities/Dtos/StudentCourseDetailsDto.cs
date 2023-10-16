using Core.Entities;
using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class StudentCourseDetailsDto: IDto
    {
        public int Id { get; set; }
        public StudentCourseDto Student { get; set; }
        public List<Course> Courses { get; set; }
    }
}
