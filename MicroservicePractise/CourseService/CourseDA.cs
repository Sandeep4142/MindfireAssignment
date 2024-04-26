using CourseService.Models;
using Microsoft.Extensions.Logging;

namespace CourseService
{
    public class CourseDA : ICourseDA
    {
        public List<Course> GetAllCourse()
        {
            try
            {
                using (var context = new School3Context())
                {
                    List<Course> courseList = context.Courses.ToList();
                    return courseList;
                }
            }
            catch (Exception ex)
            {
                return new List<Course>();
            }
        }

        public Course GetCourseByID(int id)
        {
            try
            {
                using (var context = new School3Context())
                {
                    Course course = context.Courses.FirstOrDefault(c => c.CourseId == id);
                    return course;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool AddCourse(Course course)
        {
            try
            {
                using (var context = new School3Context())
                {
                    context.Courses.Add(course);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCourse(int id)
        {
            try
            {
                using (var context = new School3Context())
                {
                    Course course = context.Courses.FirstOrDefault(s => s.CourseId == id);
                    if (course != null)
                    {
                        context.Courses.Remove(course);
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCourseCoordinator(int id, string name)
        {
            try
            {
                using (var context = new School3Context())
                {
                    Course course = context.Courses.FirstOrDefault(c => c.CourseId == id);
                    if (course != null)
                    {
                        course.CourseCoordinator = name;
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
