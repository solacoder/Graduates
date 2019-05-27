using Graduates.Core.Entities;
using Graduates.Core.Repositories;
using Graduates.Data;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace Graduates.Persistence.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public bool IsExists(Course obj)
        {
            Course program = null;
            try
            {
                program = GraduatesContext.Courses.First<Course>(m => m.Department.Id == obj.DepartmentId && m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return program != null ? true : false;
        }

        public override Course Get(long Id)
        {
            return GraduatesContext.Courses
                    .Include(m => m.Programs)
                    .First(m => m.Id == Id);
        }


        public override IEnumerable<Course> GetAll()
        {
            return GraduatesContext.Courses
                    .Include(m => m.Institution)
                    .Include(m => m.Faculty)
                    .Include(m => m.Department)
                    .Include(m => m.Programs);
        }
    }
}
